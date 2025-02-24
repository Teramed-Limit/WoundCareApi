using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtClinicalImage
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public int? ImageSequence { get; set; }

    public string? ImageTitle { get; set; }

    public string? ImageDetail { get; set; }

    public byte[]? ImageInstanceBase64 { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public int? IsDelete { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
