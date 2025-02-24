using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_PtEncounterLocationStay
{
    public Guid Puid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public Guid? BedPuid { get; set; }

    public string? WardLabel { get; set; }

    public string? BedLabel { get; set; }

    public DateTime? SysInTime { get; set; }

    public DateTime? SysOutTime { get; set; }

    public DateTime? TransferInTime { get; set; }

    public DateTime? TransferOutTime { get; set; }

    public string? TransferOutStatus { get; set; }

    public DateTime? UpdateTime { get; set; }
}
