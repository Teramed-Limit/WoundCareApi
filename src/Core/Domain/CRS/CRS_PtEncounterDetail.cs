using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_PtEncounterDetail
{
    public Guid Puid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public Guid ObservationPuid { get; set; }

    public Guid? AttributePuid { get; set; }

    public string? ValueString { get; set; }

    public DateTime? ValueDateTime { get; set; }

    public decimal? ValueNumber { get; set; }

    public DateTime? UpdateTime { get; set; }
}
