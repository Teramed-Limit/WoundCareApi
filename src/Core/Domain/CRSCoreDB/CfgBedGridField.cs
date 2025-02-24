using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgBedGridField
{
    public Guid Puid { get; set; }

    public string? FieldShortLabel { get; set; }

    public string? FieldLongLabel { get; set; }

    public string? SourceColumnName { get; set; }

    public string? FieldValueType { get; set; }
}
