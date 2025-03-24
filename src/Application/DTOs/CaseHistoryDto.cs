namespace WoundCareApi.Application.DTOs;

public class CaseHistoryDto
{
    public Guid CaseId { get; set; }
    public string[] CaseDate { get; set; }
    public string[] CaseShiftDate { get; set; }
    public bool IsCaseClosed { get; set; }
}
