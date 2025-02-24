using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class ClinicalUnitStatisticsDatum
{
    public Guid Puid { get; set; }

    public Guid? ClinicalUnitPuid { get; set; }

    public string? SourceColumnName { get; set; }

    public string? ValueString { get; set; }

    public DateTime? ValueDateTime { get; set; }

    public decimal? ValueNumber { get; set; }

    public DateTime? DataTimeBegin { get; set; }

    public DateTime? DataTimeEnd { get; set; }

    public DateTime? UpdateTime { get; set; }
}
