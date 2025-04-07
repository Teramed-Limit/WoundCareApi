using System.Text.Json.Serialization;

namespace WoundCareApi.Application.DTOs;

public class ReportDefineDto
{
    public Guid Puid { get; set; }
    public Dictionary<string, object> HeaderDefine { get; set; } = new();
    public Dictionary<string, object> FormDefine { get; set; } = new();
    public Dictionary<string, object> FooterDefine { get; set; } = new();
    public string ReportName { get; set; } = null!;
    public DateTime CreateDateTime { get; set; }
}
