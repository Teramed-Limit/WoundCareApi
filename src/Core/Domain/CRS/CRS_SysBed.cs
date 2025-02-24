using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_SysBed
{
    public Guid Puid { get; set; }

    public Guid ClinicalUnitPuid { get; set; }

    public string? BedLabel { get; set; }

    public string? BedLabelHIS { get; set; }

    public string? BedType { get; set; }

    public string? RoomLabel { get; set; }

    public int? BedCardPoColumnID { get; set; }

    public int? BedCardPoRowId { get; set; }

    public int? Sequence { get; set; }
}
