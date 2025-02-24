using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgAttribute
{
    public Guid Puid { get; set; }

    public string AttributeShortLabel { get; set; } = null!;

    public string AttributeLongLabel { get; set; } = null!;

    public string? GlobalMedCode { get; set; }

    public string? ValueType { get; set; }

    public string? Description { get; set; }
}
