using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class CodeList
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string CodeName { get; set; } = null!;

    public string? ParentCodeValue { get; set; }

    public int OrderingIndex { get; set; }
}
