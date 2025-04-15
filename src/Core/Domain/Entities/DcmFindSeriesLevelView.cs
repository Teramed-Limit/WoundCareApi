using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DcmFindSeriesLevelView
{
    public int? CountOfImage { get; set; }

    public string SeriesInstanceUID { get; set; } = null!;

    public string StudyInstanceUID { get; set; } = null!;

    public string SeriesModality { get; set; } = null!;

    public string SeriesDate { get; set; } = null!;

    public string SeriesTime { get; set; } = null!;

    public int? SeriesNumber { get; set; }

    public string? SeriesDescription { get; set; }

    public string PatientPosition { get; set; } = null!;

    public string BodyPartExamined { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public string? PatientsName { get; set; }

    public string? PatientsSex { get; set; }

    public string? PatientsBirthDate { get; set; }

    public string? OtherPatientNames { get; set; }

    public string? OtherPatientId { get; set; }

    public string StudyDate { get; set; } = null!;

    public string StudyTime { get; set; } = null!;

    public string ReferringPhysiciansName { get; set; } = null!;

    public string StudyID { get; set; } = null!;

    public string? AccessionNumber { get; set; }

    public string? StudyDescription { get; set; }

    public string Modality { get; set; } = null!;

    public string? PerformingPhysiciansName { get; set; }

    public string? NameofPhysiciansReading { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? InstitutionName { get; set; }
}
