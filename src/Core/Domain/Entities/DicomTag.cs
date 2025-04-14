using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class DicomTag
{
    public string IdentifyName { get; set; } = null!;

    public string DicomGroup { get; set; } = null!;

    public string DicomElem { get; set; } = null!;

    public string? TagName { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
