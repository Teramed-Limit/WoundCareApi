﻿using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DicomSeries
{
    public string SeriesInstanceUID { get; set; } = null!;

    public string StudyInstanceUID { get; set; } = null!;

    public string SeriesModality { get; set; } = null!;

    public string SeriesDate { get; set; } = null!;

    public string SeriesTime { get; set; } = null!;

    public string SeriesNumber { get; set; } = null!;

    public string? SeriesDescription { get; set; }

    public string PatientPosition { get; set; } = null!;

    public string BodyPartExamined { get; set; } = null!;

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? ReferencedStudyInstanceUID { get; set; }

    public string? ReferencedSeriesInstanceUID { get; set; }
}
