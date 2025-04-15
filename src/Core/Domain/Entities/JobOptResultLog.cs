using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class JobOptResultLog
{
    public string PatientID { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string StudyInstanceUID { get; set; } = null!;

    public string? OptContent { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? CallingAETitle { get; set; }
}
