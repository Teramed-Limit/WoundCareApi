using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class QCOperationRecordView
{
    public string PatientId { get; set; } = null!;

    public string StudyDate { get; set; } = null!;

    public string? AccessionNumber { get; set; }

    public string Modality { get; set; } = null!;

    public string? StudyDescription { get; set; }

    public string StudyInstanceUID { get; set; } = null!;

    public string? QCGuid { get; set; }

    public string Operator { get; set; } = null!;

    public int IsSuccess { get; set; }
}
