using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgObservation
{
    public Guid Puid { get; set; }

    public string ObservationShortLabel { get; set; } = null!;

    public string? ObservationLongLabel { get; set; }

    public string? AttributeShortLabel { get; set; }

    public string? AttributeLongLabel { get; set; }

    public string? ValueType { get; set; }

    public string? GlobalMedCode { get; set; }

    public string? SourceDataSetName { get; set; }

    public string? ObservationCategory { get; set; }

    public double? Sequence { get; set; }

    public string? Description { get; set; }
}
