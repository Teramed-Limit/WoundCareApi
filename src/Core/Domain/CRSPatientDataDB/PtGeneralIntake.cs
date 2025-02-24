using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtGeneralIntake
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public Guid? MaterialPuid { get; set; }

    public string? ObservationLabel { get; set; }

    public decimal? VolumeIn { get; set; }

    public decimal? Rate { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public string? ValueComment { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
