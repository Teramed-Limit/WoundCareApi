using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgPatientViewFunctionBlock
{
    public Guid Puid { get; set; }

    public Guid PatientViewPuid { get; set; }

    public Guid FunctionBlockPuid { get; set; }

    public int? DefaultBackwardMinutes { get; set; }

    public int? DefaultForwardMinutes { get; set; }

    public int? RowPosId { get; set; }

    public int? ColPosId { get; set; }

    public int? LeftPannelPos { get; set; }

    public int? RightPannelPos { get; set; }
}
