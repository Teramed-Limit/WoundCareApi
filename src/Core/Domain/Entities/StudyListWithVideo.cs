using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class StudyListWithVideo
{
    public string PatientId { get; set; } = null!;

    public string? PatientsName { get; set; }

    public string? Sex { get; set; }

    public string? PatientsBirthDate { get; set; }

    public int? Age { get; set; }

    public string StudyInstanceUID { get; set; } = null!;

    public string Modality { get; set; } = null!;

    public string? AccessionNumber { get; set; }

    public string StudyDate { get; set; } = null!;

    public string StudyTime { get; set; } = null!;

    public string Physician { get; set; } = null!;

    public string? Description { get; set; }

    public string StudyID { get; set; } = null!;

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
