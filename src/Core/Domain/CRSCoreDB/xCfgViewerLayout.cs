using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class xCfgViewerLayout
{
    public Guid Puid { get; set; }

    public string? LayoutLabel { get; set; }

    public int NumberOfRow { get; set; }

    public int? NumberOfColumn { get; set; }
}
