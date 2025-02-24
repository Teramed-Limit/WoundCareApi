using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class StorageDevice
{
    public string StorageDeviceID { get; set; } = null!;

    public string StoragePath { get; set; } = null!;

    public string? StorageDescription { get; set; }

    public string IPAddress { get; set; } = null!;

    public string? UserID { get; set; }

    public string? UserPassword { get; set; }

    public string? StorageLevel { get; set; }

    public string? DicomFilePathRule { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
