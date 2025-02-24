using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgBodyLocation
{
    public Guid Puid { get; set; }

    public string? ShortLabel { get; set; }

    public string? LongLabel { get; set; }

    public int? SVGGraphicId { get; set; }

    public Guid? CaseTypePuid { get; set; }

    public string? NISCategory { get; set; }

    public string? NISLocationId { get; set; }

    public string? NISLocationLabel { get; set; }

    public int? PositionLeft { get; set; }

    public int? PositionTop { get; set; }
}
