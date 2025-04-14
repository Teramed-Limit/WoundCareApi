using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class CRS_CfgCaseType
{
    public Guid Puid { get; set; }

    public string? CaseTypeShortLabel { get; set; }

    public string? CaseTypeLongLabel { get; set; }

    public string? CaseTypeCategory { get; set; }
}
