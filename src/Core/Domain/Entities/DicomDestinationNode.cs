using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DicomDestinationNode
{
    public string LogicalName { get; set; } = null!;

    public string AETitle { get; set; } = null!;

    public string? SendingAETitle { get; set; }

    public string? HostName { get; set; }

    public string IPAddress { get; set; } = null!;

    public int Port { get; set; }

    public string? Description { get; set; }

    public string? RoutingRulePattern { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
