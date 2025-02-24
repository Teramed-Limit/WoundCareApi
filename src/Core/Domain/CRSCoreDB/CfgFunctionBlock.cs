using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgFunctionBlock
{
    public Guid Puid { get; set; }

    public string? FunctionBlockLabel { get; set; }

    public string? FunctionBlockTitle { get; set; }

    public int? DefaultBackwardMinutes { get; set; }

    public int? DefaultForwardMinutes { get; set; }

    public int? CountOfTab { get; set; }

    public string? TabTitleList { get; set; }

    public string? Description { get; set; }

    public int? Sequence { get; set; }

    public byte[]? DisplaySample { get; set; }
}
