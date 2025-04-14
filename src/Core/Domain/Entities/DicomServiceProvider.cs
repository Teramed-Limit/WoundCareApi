using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class DicomServiceProvider
{
    public string Name { get; set; } = null!;

    public string AETitle { get; set; } = null!;

    public int Port { get; set; }

    public int DicomServiceType { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
