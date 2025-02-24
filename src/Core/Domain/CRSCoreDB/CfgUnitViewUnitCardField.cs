using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgUnitViewUnitCardField
{
    public Guid Puid { get; set; }

    public Guid? UnitViewPuid { get; set; }

    public Guid? UnitCardPuid { get; set; }

    public Guid? UnitCardFieldPuid { get; set; }

    public int? CardPosColId { get; set; }

    public int? CardPosRowId { get; set; }

    public string? DisplayTitle { get; set; }

    public byte[]? DisplayImage { get; set; }

    public int? showLegend { get; set; }

    public double? RangeStart { get; set; }

    public double? RangeEnd { get; set; }

    public Guid? RangeStartUnitCardFieldPuid { get; set; }

    public Guid? RangeEndUnitCardFieldPuid { get; set; }

    public string? DefaultValueFontColor { get; set; }

    public string? DefaultValueBackColor { get; set; }

    public int? IsStack100 { get; set; }

    public int? IsDonut { get; set; }

    public int? Sequence { get; set; }
}
