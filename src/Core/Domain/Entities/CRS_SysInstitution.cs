using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class CRS_SysInstitution
{
    public Guid Puid { get; set; }

    public string? InstitutionShortLabel { get; set; }

    public string? InstitutionLongLabel { get; set; }

    public string? InstitutionAddress { get; set; }

    public string? InstitutionPhoneNumber { get; set; }

    public string? InstitutionType { get; set; }

    public string? Description { get; set; }
}
