using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class DicomTagFilter
{
    public string TagFilterName { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
