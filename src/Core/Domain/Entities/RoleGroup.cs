using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class RoleGroup
{
    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }
}
