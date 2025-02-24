using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class RoleFunction
{
    public string FunctionName { get; set; } = null!;

    public string? Description { get; set; }

    public string? CorrespondElementId { get; set; }
}
