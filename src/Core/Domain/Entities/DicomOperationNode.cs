using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DicomOperationNode
{
    public string Name { get; set; } = null!;

    public string OperationType { get; set; } = null!;

    public string? AETitle { get; set; }

    public string? RemoteAETitle { get; set; }

    public string? IPAddress { get; set; }

    public int? Port { get; set; }

    public string? Description { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public int? Enable { get; set; }

    public string? MoveAETitle { get; set; }
}
