using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class CfgImageLibrary
{
    public Guid Puid { get; set; }

    public string? ImageLabel { get; set; }

    public byte[]? ImageBinary { get; set; }

    public string? ImageFullFileName { get; set; }

    public string? Description { get; set; }
}
