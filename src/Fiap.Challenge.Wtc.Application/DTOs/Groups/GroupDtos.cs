namespace Fiap.Challenge.Wtc.Application.DTOs.Groups;

public record CreateGroupRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}

public record GetGroupsResponse
{
    public List<GroupDto> Groups { get; init; } = new();
}

public record GroupDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public List<string> MemberIds { get; init; } = new();
    public DateTime CreatedAt { get; init; }
}

public record AddGroupMemberRequest
{
    public string UserId { get; init; } = string.Empty;
}