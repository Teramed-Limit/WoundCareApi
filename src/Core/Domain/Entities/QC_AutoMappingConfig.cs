using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class QC_AutoMappingConfig
{
    public string StationName { get; set; } = null!;

    public string EnvSetup { get; set; } = null!;

    public string WkSCP { get; set; } = null!;

    public string StoreSCP { get; set; } = null!;

    public string CFindReqField { get; set; } = null!;

    public string MappingField { get; set; } = null!;

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
