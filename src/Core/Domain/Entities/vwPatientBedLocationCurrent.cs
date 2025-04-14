using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class vwPatientBedLocationCurrent
{
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

    public Guid ClinicalUnitPuid { get; set; }
}
