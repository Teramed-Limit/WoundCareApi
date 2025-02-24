using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class SysRole
{
    public Guid Puid { get; set; }

    public string RoleLabel { get; set; } = null!;

    public string? RoleDescription { get; set; }
}
