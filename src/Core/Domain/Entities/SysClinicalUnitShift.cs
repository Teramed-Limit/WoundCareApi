using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class SysClinicalUnitShift
{
    public Guid Puid { get; set; }

    public Guid ClinicalUnitPuid { get; set; }

    public int? ShiftBeginHour { get; set; }

    public int? ShiftOffsetMinutes { get; set; }

    public int? ShiftDuration { get; set; }

    public string? ShiftShortLabel { get; set; }

    public string? ShiftLongLabel { get; set; }
}
