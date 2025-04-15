using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class RoleFunctionView
{
    public string FunctionName { get; set; } = null!;

    public string? Description { get; set; }

    public string? CorrespondElementId { get; set; }

    public string RoleName { get; set; } = null!;

    public int Id { get; set; }
}
