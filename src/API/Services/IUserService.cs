using System.Security.Claims;
using WoundCareApi.src.Core.Domain.CRS;

namespace WoundCareApi.API.Services
{
    public interface IUserService
    {
        Task<LoginUserDatum?> ValidateUserAsync(string userId, string password);
        Task<LoginUserDatum?> GetUserByIdAsync(string userId);
        Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
        Task SaveRefreshTokenAsync(string userId, string refreshToken);
        Task RemoveRefreshTokenAsync(string userId);
        Task<(string accessToken, string refreshToken)> GenerateTokens(LoginUserDatum user);
        ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false);
    }
}
