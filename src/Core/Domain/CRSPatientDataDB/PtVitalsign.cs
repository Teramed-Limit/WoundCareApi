using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtVitalsign
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public decimal? HR { get; set; }

    public decimal? RR { get; set; }

    public decimal? SpO2 { get; set; }

    public decimal? ABPs { get; set; }

    public decimal? ABPd { get; set; }

    public decimal? ABPm { get; set; }

    public decimal? NBPs { get; set; }

    public decimal? NBPd { get; set; }

    public decimal? NBPm { get; set; }

    public decimal? BodyTemp { get; set; }

    public decimal? PVC { get; set; }

    public decimal? CVPm { get; set; }

    public decimal? ICP { get; set; }

    public decimal? CPP { get; set; }

    public decimal? GCS { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
