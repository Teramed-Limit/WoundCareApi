using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class VideoBroadcasting
{
    public string Name { get; set; } = null!;

    public string HostName { get; set; } = null!;

    public int Port { get; set; }

    public string UserID { get; set; } = null!;

    public string? Description { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
