using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgUnitView
{
    public Guid Puid { get; set; }

    public string? UnitViewForm { get; set; }

    public string UnitViewLabel { get; set; } = null!;

    public Guid? PatientViewPuid { get; set; }

    public string? Description { get; set; }

    public int IsDefault { get; set; }
}
