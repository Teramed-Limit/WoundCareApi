using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class SearchImagePathView
{
    public string ImageFullPath { get; set; } = null!;

    public string SOPInstanceUID { get; set; } = null!;

    public string? SOPClassUID { get; set; }

    public int? ImageNumber { get; set; }

    public string ImageDate { get; set; } = null!;

    public string ImageTime { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string StorageDeviceID { get; set; } = null!;

    public string? ImageStatus { get; set; }

    public string PatientId { get; set; } = null!;

    public string? PatientsName { get; set; }

    public string StudyInstanceUID { get; set; } = null!;

    public string StudyDate { get; set; } = null!;

    public string StudyTime { get; set; } = null!;

    public string? AccessionNumber { get; set; }

    public string? StudyDescription { get; set; }

    public string SeriesModality { get; set; } = null!;

    public string BodyPartExamined { get; set; } = null!;

    public string PatientPosition { get; set; } = null!;

    public string StoragePath { get; set; } = null!;

    public string HttpFilePath { get; set; } = null!;

    public string? StorageDescription { get; set; }

    public string SeriesInstanceUID { get; set; } = null!;

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public int? NumberOfFrames { get; set; }
}
