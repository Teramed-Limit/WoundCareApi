using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class OperationRecord
{
    public int Id { get; set; }

    public string DateTime { get; set; } = null!;

    public string Operation { get; set; } = null!;

    public string? OperationName { get; set; }

    public string? Description { get; set; }

    public string Operator { get; set; } = null!;

    public string? Reason { get; set; }

    public int IsSuccess { get; set; }

    public string? QCGuid { get; set; }
}
