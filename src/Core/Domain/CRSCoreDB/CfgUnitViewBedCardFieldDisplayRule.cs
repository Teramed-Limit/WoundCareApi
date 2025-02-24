using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgUnitViewBedCardFieldDisplayRule
{
    public Guid Puid { get; set; }

    public Guid UnitViewBedGridFieldPuid { get; set; }

    public string RuleExpression { get; set; } = null!;

    public string? FontSize { get; set; }

    public string? FontColor { get; set; }

    public string? FontBold { get; set; }

    public string? BackgroundColor { get; set; }

    public string? BorderColor { get; set; }

    public string? LightColor { get; set; }

    public byte[]? DisplayImage { get; set; }

    public int? Sequence { get; set; }
}
