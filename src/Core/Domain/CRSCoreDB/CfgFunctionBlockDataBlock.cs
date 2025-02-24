using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgFunctionBlockDataBlock
{
    public Guid Puid { get; set; }

    public Guid FunctionBlockPuid { get; set; }

    public Guid DataBlockPuid { get; set; }

    public string? DataBlockParameterList { get; set; }

    public Guid? BodySystemPuid { get; set; }

    public int? DefaultBackwardMinutes { get; set; }

    public int? DefaultForwardMinutes { get; set; }

    public int? RowPosId { get; set; }

    public int? ColPosId { get; set; }

    public int? TabPosId { get; set; }
}
