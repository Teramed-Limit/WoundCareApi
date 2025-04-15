using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class ImageOverlayInfoSet
{
    public string Name { get; set; } = null!;

    public string? Modality { get; set; }

    public string? Content { get; set; }

    public string? Description { get; set; }

    public string? Active { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
