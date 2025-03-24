using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class ReportDefine
{
    public Guid Puid { get; set; }

    public string? FormDefine { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public DateTime? ModifyDateTime { get; set; }

    public int? FormDefineFile { get; set; }

    public bool? isLatest { get; set; }
}
