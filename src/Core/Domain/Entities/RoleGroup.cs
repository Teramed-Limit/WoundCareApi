using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class RoleGroup
{
    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }
}
