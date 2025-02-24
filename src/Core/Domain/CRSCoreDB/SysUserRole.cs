using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class SysUserRole
{
    public Guid Puid { get; set; }

    public Guid UserPuid { get; set; }

    public Guid RolePuid { get; set; }

    public string? Description { get; set; }

    public int IsEnabled { get; set; }
}
