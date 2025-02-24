namespace WoundCareApi.API.DTOs;

public class CaseReportDTO
{
    public Guid CaseId { get; set; }
    public string? CaseDateTime { get; set; }
    public Guid FormDefinePuid { get; set; }
    public required Dictionary<string, object> FormData { get; set; }
    public Guid ClinicalUnitPuid { get; set; }
}
