using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgBodySystem
{
    public Guid Puid { get; set; }

    public string? BodySystemShortLabel { get; set; }

    public string? BodySystemLongLabel { get; set; }

    public string? GlobalMedCode { get; set; }

    public string? Description { get; set; }

    public int? Sequence { get; set; }
}
