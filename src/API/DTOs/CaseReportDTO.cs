namespace WoundCareApi.API.DTOs;

public class CaseReportDTO
{
    public Guid CaseId { get; set; }
    public string CaseDate { get; set; }
    public Guid FormDefinePuid { get; set; }
    public Dictionary<string, object> FormData { get; set; }
}
