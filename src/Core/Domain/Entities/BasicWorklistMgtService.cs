namespace WoundCareApi.Core.Domain.Entities;

public partial class BasicWorklistMgtService
{
    public string? HISProcedureID { get; set; }

    public string AccessionNumber { get; set; } = null!;

    public string PatientID { get; set; } = null!;

    public string? PatientOtherID { get; set; }

    public string? DocumentNumber { get; set; }

    public string? PatientName { get; set; }

    public string? PatientOtherName { get; set; }

    public string? Sex { get; set; }

    public string? Birthdate { get; set; }

    public string StudyInstanceUID { get; set; } = null!;

    public string Modality { get; set; } = null!;

    public string StudyDate { get; set; } = null!;

    public string? StudyDescription { get; set; }

    public string? PerformingPhysician { get; set; }

    public string? ReferringPhysician { get; set; }

    public string? NameOfPhysiciansReadingStudy { get; set; }

    public string? AssistantPhysician { get; set; }

    public string? Anesthesiologist { get; set; }

    public string? ProcedureCode { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? Dept { get; set; }

    public string? ImageComments { get; set; }
}
