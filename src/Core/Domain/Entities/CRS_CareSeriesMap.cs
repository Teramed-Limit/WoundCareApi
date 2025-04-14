using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

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

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
