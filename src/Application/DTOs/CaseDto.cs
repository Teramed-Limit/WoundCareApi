namespace WoundCareApi.Application.DTOs;

public class CaseDto
{
    public Guid Puid { get; set; }
    public string? LifeTimeNumber { get; set; }
    public string? EncounterNumber { get; set; }
    public string? LocationLabel { get; set; }
    public string? LocationSVGId { get; set; }
    public string? CaseEntityId { get; set; }
    public DateTime? CaseBeginTime { get; set; }
    public DateTime? CaseCloseTime { get; set; }
    public string? CaseCloseStatus { get; set; }
    public string? CaseCloseCareProviderName { get; set; }
    public string? CareProviderId { get; set; }
    public string? CaseTypeShortLabel { get; set; }
    public bool? IsCaseClosed { get; set; }
}
