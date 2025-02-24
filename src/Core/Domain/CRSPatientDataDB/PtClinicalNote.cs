using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtClinicalNote
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public Guid? ObservationPuid { get; set; }

    public string? OrderCode { get; set; }

    public string? ObservationSource { get; set; }

    public string? ObservationCode { get; set; }

    public string? ObservationLabel { get; set; }

    public string? ValueString { get; set; }

    public string? AbnormalFlags { get; set; }

    public string? ResultStatus { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public string? ValueComment { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
