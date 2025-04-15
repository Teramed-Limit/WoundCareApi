using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class ModalityLookupTable
{
    public int DisplayIndex { get; set; }

    public string? Caption { get; set; }

    public int? WindowWidth { get; set; }

    public int? WindowCenter { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
