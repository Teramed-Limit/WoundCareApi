using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgUnitViewBedGridField
{
    public Guid Puid { get; set; }

    public Guid UnitViewPuid { get; set; }

    public Guid BedGridFieldPuid { get; set; }

    public int? ShowText { get; set; }

    public int? ShowImage { get; set; }

    public int? DefaultHide { get; set; }

    public string? FieldLinkURL { get; set; }

    public int? Sequence { get; set; }
}
