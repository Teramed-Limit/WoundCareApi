using System.ComponentModel;

namespace WoundCareApi.Core.Domain.CRS;

public enum CaseStatus
{
    [Description("In Progress")]
    InProgress = 0,

    [Description("Closed")]
    Closed = 1
}
