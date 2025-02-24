using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgPatientView
{
    public Guid Puid { get; set; }

    public string PatientViewLabel { get; set; } = null!;

    public string? PatientViewForm { get; set; }

    public int? DefaultBackwardHours { get; set; }

    public double? LeftPanelWidth { get; set; }

    public int? LeftPanelCountOfRow { get; set; }

    public int? LeftPanelCountOfColumn { get; set; }

    public double? MidPanelWidth { get; set; }

    public int? MidPanelCountOfRow { get; set; }

    public int? MidPanelCountOfColumn { get; set; }

    public double? RightPanelWidth { get; set; }

    public int? RightPanelCountOfRow { get; set; }

    public int? RightPanelCountOfColumn { get; set; }

    public string? Description { get; set; }
}
