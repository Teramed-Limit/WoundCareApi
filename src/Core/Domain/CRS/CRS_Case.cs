using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_Case
{
    public Guid Puid { get; set; }

    public string? LIfeTimeNumber { get; set; }

    public string? EncounterNumber { get; set; }

    public Guid? CaseTypePuid { get; set; }

    public string? CaseLocation { get; set; }

    public string? CaseEntityId { get; set; }

    public DateTime? CaseBeginTime { get; set; }

    public DateTime? CaseCloseTime { get; set; }

    public string? CaseCloseStatus { get; set; }

    public string? CaseCloseCareProviderName { get; set; }

    public string? CareProviderId { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
