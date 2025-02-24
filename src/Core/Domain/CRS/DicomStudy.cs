using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class DicomStudy
{
    public string StudyInstanceUID { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public string StudyDate { get; set; } = null!;

    public string StudyTime { get; set; } = null!;

    public string ReferringPhysiciansName { get; set; } = null!;

    public string StudyID { get; set; } = null!;

    public string? AccessionNumber { get; set; }

    public string? StudyDescription { get; set; }

    public string Modality { get; set; } = null!;

    public string? PerformingPhysiciansName { get; set; }

    public string? NameofPhysiciansReading { get; set; }

    public string? StudyStatus { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? ProcedureID { get; set; }

    public string? ReferencedStudyInstanceUID { get; set; }

    public int? Merged { get; set; }

    public int? Mapped { get; set; }

    public int? NumMappingFailures { get; set; }

    public int? Deleted { get; set; }

    public string? QCGuid { get; set; }

    public string? InstitutionName { get; set; }

    public string? StationName { get; set; }
}
