namespace WoundCareApi.Application.DTOs;

public class LoginRq
{
    public string UserId { get; set; }
    public string Password { get; set; }
}

public class AuthRp
{
    public string AccessToken { get; set; }
    public UserDto User { get; set; }
}

public class RefreshTokenRp
{
    public string AccessToken { get; set; }
}

