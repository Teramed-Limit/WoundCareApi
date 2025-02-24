using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class FunctionRoleGroup
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string FunctionName { get; set; } = null!;
}
