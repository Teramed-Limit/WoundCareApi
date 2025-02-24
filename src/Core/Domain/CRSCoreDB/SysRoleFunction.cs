using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class SysRoleFunction
{
    public Guid Puid { get; set; }

    public Guid RolePuid { get; set; }

    public Guid FunctionPuid { get; set; }

    public int CanRead { get; set; }

    public int CanWrite { get; set; }

    public int CanDelete { get; set; }

    public string? Description { get; set; }
}
