using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class DicomNode
{
    public string Name { get; set; } = null!;

    public string AETitle { get; set; } = null!;

    public string? IPAddress { get; set; }

    public int? PortNumber { get; set; }

    public string RemoteAETitle { get; set; } = null!;

    public string? NeedConfirmIPAddress { get; set; }

    public int? ImageCompression { get; set; }

    public int? CompressQuality { get; set; }

    public string? Description { get; set; }

    public int? Priority { get; set; }

    public string? AcceptedTransferSyntaxesCustomize { get; set; }

    public string? TransferSyntaxesCustomize { get; set; }

    public string? WorklistMatchKeys { get; set; }

    public string? WorklistReturnKeys { get; set; }

    public string ServiceJobTypes { get; set; } = null!;

    public string? EnabledAutoRouting { get; set; }

    public string? AuotRoutingDestination { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? FilterRulePattern { get; set; }

    public int? WorklistQueryPattern { get; set; }

    public string? Department { get; set; }
}
