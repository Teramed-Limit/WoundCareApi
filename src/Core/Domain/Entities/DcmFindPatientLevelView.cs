using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DcmFindPatientLevelView
{
    public int? CountOfStudy { get; set; }

    public int? CountOfSeries { get; set; }

    public string PatientId { get; set; } = null!;

    public string? PatientsName { get; set; }

    public string? PatientsSex { get; set; }

    public string? PatientsBirthDate { get; set; }

    public string? OtherPatientNames { get; set; }

    public string? OtherPatientId { get; set; }

    public string? ModifiedUser { get; set; }

    public string? DocumentNumber { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string CreateUser { get; set; } = null!;

    public string CreateDateTime { get; set; } = null!;
}
