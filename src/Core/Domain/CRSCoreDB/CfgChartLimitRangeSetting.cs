using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgChartLimitRangeSetting
{
    public Guid Puid { get; set; }

    public string? SourceDataSetName { get; set; }

    public string? ChartSeriesName { get; set; }

    public decimal? LowLimit { get; set; }

    public decimal? HighLimit { get; set; }

    public decimal? CriticalLowLimit { get; set; }

    public decimal? CriticalHighLimit { get; set; }

    public decimal? YScaleLow { get; set; }

    public decimal? YScaleHigh { get; set; }

    public int? YTickStep { get; set; }

    public int? XMaximumTicks { get; set; }
}
