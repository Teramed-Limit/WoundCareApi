using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class HangingProtocolConfig
{
    public int DisplayIndex { get; set; }

    public string? Caption { get; set; }

    public int? RowCounts { get; set; }

    public int? ColumnCounts { get; set; }

    public int? LeftMargin { get; set; }

    public int? TopMargin { get; set; }

    public int? RightMargin { get; set; }

    public int? BottomMargin { get; set; }

    public int? BoxMargin { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
