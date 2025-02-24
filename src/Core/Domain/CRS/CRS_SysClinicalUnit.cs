using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_SysClinicalUnit
{
    public Guid Puid { get; set; }

    public Guid InstitutionPuid { get; set; }

    public string? HISCode { get; set; }

    public string? DisplayLabel { get; set; }

    public string? Location { get; set; }

    public int? TotalOfBed { get; set; }

    public int? DayBeginHour { get; set; }

    public int? DayBeginOffsetMinutes { get; set; }

    public int? DayDuration { get; set; }

    public int? Sequence { get; set; }

    public Guid? UnitViewPuid { get; set; }

    public Guid? PatientViewPuid { get; set; }

    public int? BedCardColumns { get; set; }

    public int? BedCardRows { get; set; }

    public Guid? UnitBedCardPuid { get; set; }

    public string? BedZoneList { get; set; }

    public int? IsEnabled { get; set; }
}
