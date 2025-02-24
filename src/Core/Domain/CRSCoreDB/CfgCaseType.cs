using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgCaseType
{
    public Guid Puid { get; set; }

    public string? CaseTypeShortLabel { get; set; }

    public string? CaseTypeLongLabel { get; set; }

    public string? CaseTypeCategory { get; set; }
}
