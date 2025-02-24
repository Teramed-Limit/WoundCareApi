using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_CareSeriesMap
{
    public Guid Puid { get; set; }

    public Guid? PtCasePuid { get; set; }

    public string? DicomSeriesUid { get; set; }

    public string? DicomSeriesDate { get; set; }

    public string? DicomSeriesTime { get; set; }

    public DateTime? DicomSeriesShiftDate { get; set; }

    public Guid? DicomSeriesClinicalUnitShiftPuid { get; set; }

    public DateTime? StoreTime { get; set; }
}
