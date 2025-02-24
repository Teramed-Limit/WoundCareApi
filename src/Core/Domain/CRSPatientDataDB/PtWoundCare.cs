using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtWoundCare
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public string? CaseId { get; set; }

    public Guid? WoundTypePuid { get; set; }

    public Guid? BodyLocationPuid { get; set; }

    public string? BodyLocationDescription { get; set; }

    public string? WoundSource { get; set; }

    public string? WoundSize { get; set; }

    public string? WoundDeepth { get; set; }

    public string? WoundLevel { get; set; }

    public string? SmellSecretions { get; set; }

    public string? QuantitySecretions { get; set; }

    public string? WoundColor { get; set; }

    public string? DressingChange { get; set; }

    public string? CareAction { get; set; }

    public string? Comment { get; set; }

    public DateTime? CaseBeginDateTime { get; set; }

    public string? CaseCloseStatus { get; set; }

    public DateTime? CaseCloseDateTime { get; set; }

    public string? CaseCloseCareProviderName { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public string? CareProviderId { get; set; }

    public string? CareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
