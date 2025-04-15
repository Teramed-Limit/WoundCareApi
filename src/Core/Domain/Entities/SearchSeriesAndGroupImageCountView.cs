using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class SearchSeriesAndGroupImageCountView
{
    public string SeriesInstanceUID { get; set; } = null!;

    public string StudyInstanceUID { get; set; } = null!;

    public string Modality { get; set; } = null!;

    public string SeriesDate { get; set; } = null!;

    public string SeriesTime { get; set; } = null!;

    public string SeriesNumber { get; set; } = null!;

    public int SeriesNumberInt { get; set; }

    public string? Description { get; set; }

    public int? Images { get; set; }

    public string? ReceiveTime { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
