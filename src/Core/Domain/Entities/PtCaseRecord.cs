using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class PtCaseRecord
{
    public Guid Puid { get; set; }

    public Guid? PtCasePuid { get; set; }

    public Guid? FormDefinePuid { get; set; }

    public string? FormData { get; set; }

    public string? Comment { get; set; }

    public DateTime? ObservationDateTime { get; set; }

    public DateTime? ObservationShiftDate { get; set; }

    public Guid? ClinicalUnitShiftPuid { get; set; }

    public string? CareProviderId { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
