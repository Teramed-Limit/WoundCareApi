using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DicomPatient
{
    public string PatientId { get; set; } = null!;

    public string? PatientsName { get; set; }

    public string? PatientsSex { get; set; }

    public string? PatientsBirthDate { get; set; }

    public string? PatientsBirthTime { get; set; }

    public string? OtherPatientNames { get; set; }

    public string? OtherPatientId { get; set; }

    public string? EthnicGroup { get; set; }

    public string? PatientComments { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public string? DocumentNumber { get; set; }
}
