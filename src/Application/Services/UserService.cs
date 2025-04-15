using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using TeraLinkaCareApi.Application.Services.Interfaces;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Core.Domain.Interfaces;
using TeraLinkaCareApi.Infrastructure.Persistence;
using TeraLinkaCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace TeraLinkaCareApi.Application.Services
{
    public class UserService : IUserService
    {
        public const string Secret =
            "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        private readonly IRepository<LoginUserDatum, CRSDbContext> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleService _roleService;

        public UserService(
            IRepository<LoginUserDatum, CRSDbContext> userRepository,
            IUnitOfWork unitOfWork,
            RoleService roleService
        )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleService = roleService;
        }

        public async Task<LoginUserDatum?> ValidateUserAsync(string userId, string password)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<LoginUserDatum?> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            var storedToken = await _userRepository.GetByIdAsync(userId);
            return storedToken?.RefreshToken == refreshToken;
        }

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task RemoveRefreshTokenAsync(string userId)
        {
            var user = new LoginUserDatum { UserID = userId, RefreshToken = null };
            await _userRepository.UpdatePartialAsync(user, "RefreshToken");
            await _unitOfWork.SaveAsync();
        }

        public async Task<(string accessToken, string refreshToken)> GenerateTokens(
            LoginUserDatum user
        )
        {
            var roles = await _roleService.GetUserRoleList(user.UserID);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 小寫開頭設定
                WriteIndented = true // 讓輸出比較好看（可選）
            };


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID),
                new Claim(ClaimTypes.Name, user.UserID),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(roles, options)),
            };

            var accessToken = GenerateToken(claims, 15); // Access Token 15分鐘過期
            var refreshToken = GenerateToken(claims, 7 * 24 * 60); // Refresh Token 7天過期

            return (accessToken, refreshToken);
        }

        private string GenerateToken(Claim[] claims, int expirationMinutes)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // var expiresTest =
            //     expirationMinutes == 15
            //         ? DateTime.Now.AddSeconds(5)
            //         : DateTime.Now.AddMinutes(expirationMinutes);

            var token = new JwtSecurityToken(
                issuer: "teramed",
                audience: "teramed",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                // expires: expiresTest,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "teramed",
                ValidateAudience = true,
                ValidAudience = "teramed",
                ValidateLifetime = true, // 驗證時間
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromSeconds(3), // 時間偏移量
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }
    }
}