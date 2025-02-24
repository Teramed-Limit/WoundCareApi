namespace WoundCareApi.API.DTOs;

public class ReportDefineDTO
{
    public Guid Puid { get; set; }
    public Dictionary<string, object> FormDefine { get; set; } = new Dictionary<string, object>();
}
