using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class ReportDefine
{
    public Guid Puid { get; set; }

    public string? FormDefine { get; set; }

    public DateTime CreateDateTime { get; set; }

    public DateTime? ModifyDateTime { get; set; }

    public string? FormDefineFile { get; set; }

    public string ReportName { get; set; } = null!;

    public string? HeaderDefine { get; set; }

    public string? FooterDefine { get; set; }
}
