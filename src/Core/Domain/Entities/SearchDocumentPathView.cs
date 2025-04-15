using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class SearchDocumentPathView
{
    public string SeriesModality { get; set; } = null!;

    public string ImageFullPath { get; set; } = null!;

    public string StoragePath { get; set; } = null!;

    public string? StorageDescription { get; set; }

    public string SeriesInstanceUID { get; set; } = null!;

    public string SOPInstanceUID { get; set; } = null!;

    public string? SOPClassUID { get; set; }

    public string ImageDate { get; set; } = null!;

    public string ImageTime { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string StorageDeviceID { get; set; } = null!;

    public string? ImageStatus { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public int? NumberOfFrames { get; set; }

    public string ImageNumber { get; set; } = null!;
}
