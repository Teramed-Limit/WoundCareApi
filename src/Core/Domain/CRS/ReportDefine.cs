using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class ReportDefine
{
    public Guid Puid { get; set; }

    public string? FormDefine { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public DateTime? ModifyDateTime { get; set; }

    public int? FormDefineFile { get; set; }

    public bool? IsLatest { get; set; }
}
