using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class FileOperationRecord
{
    public string UserId { get; set; } = null!;

    public string OperationDate { get; set; } = null!;

    public string OperationTime { get; set; } = null!;

    public int Type { get; set; }

    public string PatientId { get; set; } = null!;

    public string? PatientsName { get; set; }

    public string? StudyDate { get; set; }

    public string? StudyDescription { get; set; }

    public string? StudyInstanceUID { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
