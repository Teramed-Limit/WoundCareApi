

using TeraLinkaCareApi.Core.Domain.Entities;

namespace TeraLinkaCareApi.Application.DTOs;

public class UserDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CName { get; set; }
    public string EName { get; set; }

    public List<RoleFunction> RoleFunctionList { get; set; } = new List<RoleFunction>();
}
