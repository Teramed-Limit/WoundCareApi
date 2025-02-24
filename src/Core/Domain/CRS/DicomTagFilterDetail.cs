using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRS;

public partial class DicomTagFilterDetail
{
    public string TagFilterName { get; set; } = null!;

    public string TagIdentifyName { get; set; } = null!;

    public string TagRule { get; set; } = null!;

    public string? Value { get; set; }

    public string? AndAll { get; set; }

    public string? CreateDateTime { get; set; }

    public string? CreateUser { get; set; }

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
