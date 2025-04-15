namespace TeraLinkaCareApi.Application.DTOs;

public class UserAccountDto
{
    public string UserID { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string? DoctorCode { get; set; }

    public string? DoctorCName { get; set; }

    public string? DoctorEName { get; set; }

    public string? IsSupervisor { get; set; }

    public List<string>? RoleList { get; set; } = null!;

    public string? CreateDateTime { get; set; } = null!;

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? Title { get; set; }

    public string? Qualification { get; set; }

    public string? SignatureBase64 { get; set; }

    public string? SignatureUrl { get; set; }

    public string? Summary { get; set; }

    public string? JobTitle { get; set; }

    public List<string>? UserGroupList { get; set; } = null!;
}