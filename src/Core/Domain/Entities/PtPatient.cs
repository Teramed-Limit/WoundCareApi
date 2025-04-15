using System;
using System.Collections.Generic;



public partial class PtPatient
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
