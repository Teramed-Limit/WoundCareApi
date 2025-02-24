using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class xCfgUnitViewBedArea
{
    public Guid Puid { get; set; }

    public string? BedAreaCode { get; set; }

    public string BedAreaLabel { get; set; } = null!;

    public string? Description { get; set; }

    public int IsDefault { get; set; }
}
