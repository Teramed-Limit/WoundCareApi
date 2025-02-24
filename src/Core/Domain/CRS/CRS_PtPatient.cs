using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_PtPatient
{
    public Guid Puid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? NationalId { get; set; }

    public DateTime? UpdateTime { get; set; }
}
