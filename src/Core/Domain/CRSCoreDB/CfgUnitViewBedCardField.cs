using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgUnitViewBedCardField
{
    public Guid Puid { get; set; }

    public Guid UnitViewPuid { get; set; }

    public Guid BedGridFieldPuid { get; set; }

    public int? ShowText { get; set; }

    public int? ShowImage { get; set; }

    public int? DefaultHide { get; set; }

    public int? CardPosColId { get; set; }

    public int? CardPosRowId { get; set; }

    public int? IsMainIndex { get; set; }

    public string? FieldLinkURL { get; set; }

    public string? Description { get; set; }

    public int? Sequence { get; set; }
}
