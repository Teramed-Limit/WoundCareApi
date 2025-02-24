using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgDataBlock
{
    public Guid Puid { get; set; }

    public string? DataBlockForm { get; set; }

    public string? DataBlockLabel { get; set; }

    public string? DataBlockTitle { get; set; }

    public string? Description { get; set; }
}
