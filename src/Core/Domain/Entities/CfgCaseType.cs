using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class CfgCaseType
{
    public Guid Puid { get; set; }

    public string? CaseTypeShortLabel { get; set; }

    public string? CaseTypeLongLabel { get; set; }

    public string? CaseTypeCategory { get; set; }
}
