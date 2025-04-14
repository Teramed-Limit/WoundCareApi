using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class CRS_PtEncounter
{
    public Guid Puid { get; set; }

    public Guid? PatientPuid { get; set; }

    public string? EncounterNumber { get; set; }

    public Guid? HostDBPuid { get; set; }

    public Guid? PatientViewPuid { get; set; }

    public DateTime? UpdateTime { get; set; }
}
