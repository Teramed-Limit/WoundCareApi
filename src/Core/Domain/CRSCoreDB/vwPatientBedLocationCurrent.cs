namespace WoundCareApi.Core.Domain.CRSCoreDB;

public partial class vwPatientBedLocationCurrent
{
    public Guid Puid { get; set; }

    public Guid? HostDBPuid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public Guid ClinicalUnitPuid { get; set; }

    public Guid? BedPuid { get; set; }

    public Guid? PatientPuid { get; set; }

    public string? WardLabel { get; set; }

    public string? BedLabel { get; set; }

    public DateTime? SysInTime { get; set; }

    public DateTime? SysOutTime { get; set; }

    public DateTime? TransferInTime { get; set; }

    public DateTime? TransferOutTime { get; set; }

    public string? TransferOutStatus { get; set; }

    public string? EncounterNumber { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? NationalId { get; set; }

    public DateTime? UpdateTime { get; set; }
}
