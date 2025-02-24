using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgUnitCard
{
    public Guid Puid { get; set; }

    public string? CardForm { get; set; }

    public string? CardCategory { get; set; }

    public string? ChartType { get; set; }

    public byte[]? ChartImage { get; set; }

    public int? ShowChartGrid { get; set; }

    public int? DrawChartBorder { get; set; }

    public string? CardDispalyThemeName { get; set; }

    public string? Description { get; set; }

    public int? Sequence { get; set; }
}
