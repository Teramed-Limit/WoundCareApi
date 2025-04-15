using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class CfgBodyLocation
{
    public Guid Puid { get; set; }

    public string? ShortLabel { get; set; }

    public string? LongLabel { get; set; }

    public string? SVGGraphicId { get; set; }

    public Guid? CaseTypePuid { get; set; }

    public string? NISCategory { get; set; }

    public string? NISLocationId { get; set; }

    public string? NISLocationLabel { get; set; }

    public int? PositionLeft { get; set; }

    public int? PositionTop { get; set; }
}
