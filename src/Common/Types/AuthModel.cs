public class LoginCredentials
{
    public string UserId { get; set; }
    public string Password { get; set; }
}

public class AuthResponse
{
    public string AccessToken { get; set; }

    // public string RefreshToken { get; set; }
    public UserDto User { get; set; }
}

public class RefreshTokenResponse
{
    public string AccessToken { get; set; }
}

public class UserDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CName { get; set; }
    public string EName { get; set; }
}
