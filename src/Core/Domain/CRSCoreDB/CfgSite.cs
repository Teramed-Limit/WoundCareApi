using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgSite
{
    public Guid Puid { get; set; }

    public string? SiteDisplayLabel { get; set; }

    public string? SiteCategory { get; set; }

    public string? SiteMaterialTag { get; set; }

    public int? DueDays { get; set; }
}
