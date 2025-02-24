using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class SysFunction
{
    public Guid Puid { get; set; }

    public string? FunctionCode { get; set; }

    public string? FunctionLabel { get; set; }

    public string? FunctionGroup { get; set; }

    public string? FunctionType { get; set; }

    public string? Description { get; set; }
}
