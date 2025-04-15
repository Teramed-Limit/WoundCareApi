using System.ComponentModel;

namespace TeraLinkaCareApi.Application.Enums;

public enum CaseStatus
{
    [Description("In Progress")]
    InProgress = 0,

    [Description("Closed")]
    Closed = 1
}
