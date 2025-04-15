namespace TeraLinkaCareApi.Application.DTOs;

public class CaseRecordDateDto
{
    public Guid Puid { get; set; }
    public DateTime? ObservationDateTime { get; set; }
    public DateTime? ObservationShiftDate { get; set; }
    public string? ShiftLongLabel { get; set; }
    public string? ShiftShortLabel { get; set; }
    public string? CaseStatus { get; set; }
}
