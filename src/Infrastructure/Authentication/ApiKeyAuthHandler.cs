using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace TeraLinkaCareApi.Infrastructure.Authentication;

public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthOptions>
{
    private const string ApiKeyHeaderName = "CareSystem-API-Key";
    private const string ValidApiKey =
        "9e1f84d18b7fa75c7d349512e55d6d23f1c5c6a32017a7c9089df53a173ad6bc"; // 在這裡設定您的 API Key

    public ApiKeyAuthHandler(
        IOptionsMonitor<ApiKeyAuthOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    )
        : base(options, logger, encoder) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 檢查是否已經通過 JWT 認證
        var jwtResult = await Context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (jwtResult.Succeeded)
        {
            // 如果 JWT 認證成功，直接返回成功
            return AuthenticateResult.Success(jwtResult.Ticket);
        }

        // 如果 JWT 認證失敗，則檢查 API Key
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            return AuthenticateResult.Fail("API Key is missing");
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

        if (apiKeyHeaderValues.Count == 0 || string.IsNullOrEmpty(providedApiKey))
        {
            return AuthenticateResult.Fail("API Key is missing");
        }

        if (providedApiKey != ValidApiKey)
        {
            return AuthenticateResult.Fail("Invalid API Key");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "API Client"),
            new Claim(ClaimTypes.Role, "ApiClient")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.Headers["WWW-Authenticate"] = "ApiKey";
        await base.HandleChallengeAsync(properties);
    }
}
