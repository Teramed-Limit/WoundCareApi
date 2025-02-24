namespace WoundCareApi.API.DTOs;

public class CaseRecordDto
{
    public Guid Puid { get; set; }
    public Guid? FormDefinePuid { get; set; }
    public Dictionary<string, object>? FormDefine { get; set; }
    public Dictionary<string, object>? FormData { get; set; }
    public DateTime? ObservationDateTime { get; set; }
    public DateTime? ObservationShiftDate { get; set; }
    public string? ShiftLongLabel { get; set; }
    public string? ShiftShortLabel { get; set; }
    public string? RawFormDefine { get; set; }
    public string? RawFormData { get; set; }
}
