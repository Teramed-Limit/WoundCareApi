using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtSiteOutput
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public Guid? SitePuid { get; set; }

    public Guid? BodyLocationPuid { get; set; }

    public string? ObservationLabel { get; set; }

    public decimal? VolumeOutput { get; set; }

    public string? ValueUnitCode { get; set; }

    public string? ValueUnitLabel { get; set; }

    public decimal? CountOfOutput { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public string? ValueComment { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
