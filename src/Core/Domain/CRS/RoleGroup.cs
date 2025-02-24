using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class RoleGroup
{
    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }
}
