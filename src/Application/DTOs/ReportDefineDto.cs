namespace WoundCareApi.Application.DTOs;

public class ReportDefineDto
{
    public Guid Puid { get; set; }
    public Dictionary<string, object> FormDefine { get; set; } = new ();
}
