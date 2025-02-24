using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class SysUser
{
    public Guid Puid { get; set; }

    public string? UserId { get; set; }

    public string? UserEynPwd { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? UserDomainName { get; set; }

    public string? Email { get; set; }

    public string? JobTitle { get; set; }

    public string? NationalID { get; set; }

    public string? PhoneNumber1 { get; set; }

    public string? PhoneNumber2 { get; set; }

    public string? Description { get; set; }

    public int? IsEnabled { get; set; }
}
