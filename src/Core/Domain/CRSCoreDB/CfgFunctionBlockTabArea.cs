using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgFunctionBlockTabArea
{
    public Guid Puid { get; set; }

    public Guid? FunctionBlockPuid { get; set; }

    public int? TabId { get; set; }

    public int? CountOfColumn { get; set; }

    public int? CountOfRow { get; set; }

    public string? TabTitle { get; set; }

    public string? Description { get; set; }
}
