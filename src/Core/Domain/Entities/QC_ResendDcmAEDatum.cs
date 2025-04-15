using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class QC_ResendDcmAEDatum
{
    public int Port { get; set; }

    public string AETitle { get; set; } = null!;

    public string RemoteAETitle { get; set; } = null!;

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
