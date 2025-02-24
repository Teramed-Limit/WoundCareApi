using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtCultureResult
{
    public int poid { get; set; }

    public Guid? PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public string? OrderCode { get; set; }

    public string? ObservationSource { get; set; }

    public string? ObservationCode { get; set; }

    public string? ObservationLabel { get; set; }

    public string? Specimen { get; set; }

    public DateTime? OrderDateTime { get; set; }

    public DateTime? ReceiptDateTime { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public string? ValueString1 { get; set; }

    public string? ValueString2 { get; set; }

    public string? ValueString3 { get; set; }

    public string? ResultStatus { get; set; }

    public string? StainDescription { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
