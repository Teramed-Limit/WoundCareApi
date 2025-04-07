using System.Text.Json.Serialization;

namespace WoundCareApi.Application.DTOs;

public class CaseRecordDto
{
    public Guid Puid { get; set; }
    public Guid? FormDefinePuid { get; set; }
    public Dictionary<string, object>? FormDefine { get; set; }
    public Dictionary<string, object>? HeaderDefine { get; set; }
    public Dictionary<string, object>? FooterDefine { get; set; }
    public Dictionary<string, object>? FormData { get; set; }
    public DateTime? ObservationDateTime { get; set; }
    public DateTime? ObservationShiftDate { get; set; }

    [JsonIgnore]
    public string? RawFormDefine { get; set; }

    [JsonIgnore]
    public string? RawFormData { get; set; }
}
