using System;
using System.Collections.Generic;

namespace WoundCareApi.Core.Domain.Entities;

public partial class EnvironmentConfig
{
    public string Item { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
