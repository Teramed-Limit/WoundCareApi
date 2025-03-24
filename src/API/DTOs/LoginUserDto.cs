namespace WoundCareApi.API.DTOs;

public class LoginUserDto
{
    public string UserID { get; set; } = null!;
    public string? DoctorCName { get; set; }
    public string? DoctorEName { get; set; }
    public string? RoleList { get; set; }
}
