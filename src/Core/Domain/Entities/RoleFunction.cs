namespace WoundCareApi.Core.Domain.Entities;

public partial class RoleFunction
{
    public string FunctionName { get; set; } = null!;

    public string? Description { get; set; }

    public string? CorrespondElementId { get; set; }
}
