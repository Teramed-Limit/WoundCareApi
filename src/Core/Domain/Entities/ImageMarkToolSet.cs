using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class ImageMarkToolSet
{
    public string ToolName { get; set; } = null!;

    public decimal? PointDiameter { get; set; }

    public string? ColorRGB { get; set; }

    public decimal? LineWidth { get; set; }

    public int? FontSize { get; set; }

    public string? FontName { get; set; }

    public int? ValueDecimalPlace { get; set; }

    public int? ValueLeftOffset { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
