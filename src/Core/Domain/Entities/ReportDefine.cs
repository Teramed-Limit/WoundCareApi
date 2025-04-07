using System.Text.Json.Serialization;

namespace WoundCareApi.Core.Domain.Entities;

public partial class ReportDefine
{
    public Guid Puid { get; set; }

    public string? FormDefine { get; set; }

    public DateTime CreateDateTime { get; set; }

    public DateTime? ModifyDateTime { get; set; }

    public string? FormDefineFile { get; set; }

    public string ReportName { get; set; } = null!;
}
