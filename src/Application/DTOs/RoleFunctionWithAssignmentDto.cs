namespace TeraLinkaCareApi.Application.DTOs;

public class RoleFunctionWithAssignmentDto
{
    public string FunctionName { get; set; } = null!;

    public string? Description { get; set; }

    public string? CorrespondElementId { get; set; }

    public bool IsAssigned { get; set; }
}
