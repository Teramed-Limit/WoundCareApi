using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class SysHostDB
{
    public Guid Puid { get; set; }

    public string DBName { get; set; } = null!;

    public string HostName { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public int? IsPrimary { get; set; }
}
