using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Application.Services;
using WoundCareApi.Application.Services.Interfaces;
using WoundCareApi.Common.Types;

namespace WoundCareApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IWebHostEnvironment _env;

    public AuthController(IUserService userService, IWebHostEnvironment env)
    {
        _userService = userService;
        _env = env;
    }

    /// <summary>
    /// 使用者登入
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthRp>> Login([FromBody] LoginRq rq)
    {
        // 驗證使用者
        var user = await _userService.ValidateUserAsync(rq.UserId, rq.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "登入失敗" });
        }

        // 產生 Token
        var (accessToken, refreshToken) = await _userService.GenerateTokens(user);

        // 儲存 RefreshToken
        await _userService.SaveRefreshTokenAsync(user.UserID, refreshToken);

        AppendCookie("refreshToken", refreshToken);

        return Ok(
            new AuthRp
            {
                AccessToken = accessToken,
                // RefreshToken = refreshToken,
                User = new UserDto
                {
                    Id = user.UserID,
                    UserId = user.UserID,
                    CName = user.DoctorCName ?? "",
                    EName = user.DoctorEName ?? "",
                }
            }
        );
    }

    /// <summary>
    /// 更新 Token
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenRp>> RefreshToken()
    {
        try
        {
            // 從 Cookie 中取得刷新令牌
            if (!Request.Cookies.TryGetValue("refreshToken", out string refreshTokenFromCookie))
            {
                return BadRequest("Refresh token not found in cookies");
            }

            // 驗證 RefreshToken
            var principal = _userService.ValidateToken(refreshTokenFromCookie, true);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new { message = "無效的 Token" });
            }

            // 檢查 RefreshToken 是否有效
            var user = await _userService.GetUserByIdAsync(userId);
            if (
                user == null
                || !await _userService.ValidateRefreshTokenAsync(userId, refreshTokenFromCookie)
            )
            {
                return Unauthorized(new { message = "無效的 RefreshToken" });
            }

            // 產生新的 Token
            var (accessToken, refreshToken) = await _userService.GenerateTokens(user);

            // 更新 RefreshToken
            await _userService.SaveRefreshTokenAsync(userId, refreshToken);

            AppendCookie("refreshToken", refreshToken);

            return Ok(
                new RefreshTokenRp
                {
                    AccessToken = accessToken,
                    // RefreshToken = refreshToken
                }
            );
        }
        catch
        {
            return Unauthorized(new { message = "Token 更新失敗" });
        }
    }

    /// <summary>
    /// 登出
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            await _userService.RemoveRefreshTokenAsync(userId);
        }

        return Ok();
    }

    private void AppendCookie(string key, string value)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // 確保在 HTTPS 下傳輸
            SameSite = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict, // 開發環境下允許跨域
            Expires = DateTime.Now.AddDays(7) // 根據需求設置過期時間
        };

        Response.Cookies.Append(key, value, cookieOptions);
    }
}