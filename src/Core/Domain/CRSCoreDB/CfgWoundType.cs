using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgWoundType
{
    public Guid Puid { get; set; }

    public string? WoundTypeDisplayLabel { get; set; }

    public string? WoundTypeCategory { get; set; }
}
