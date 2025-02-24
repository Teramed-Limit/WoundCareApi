using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgBodySystemSite
{
    public Guid Puid { get; set; }

    public Guid? BodySystemPuid { get; set; }

    public Guid? SitePuid { get; set; }

    public string? AlternativeLabel { get; set; }

    public int? Sequence { get; set; }
}
