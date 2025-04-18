﻿using System;
using System.Collections.Generic;



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
}
