namespace WoundCareApi.API.DTOs;

public class CaseHistoryDto
{
    public Guid CaseId { get; set; }
    public string SeriesInstanceUid { get; set; }
    public string SeriesDate { get; set; }
    public string SeriesTime { get; set; }
}
