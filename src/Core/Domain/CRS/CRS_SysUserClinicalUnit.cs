using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class CRS_SysUserClinicalUnit
{
    public Guid Puid { get; set; }

    public Guid UserPuid { get; set; }

    public Guid ClinicalUnitPuid { get; set; }

    public int? CanRead { get; set; }

    public int? CanWrite { get; set; }

    public int? CanDelete { get; set; }
}
