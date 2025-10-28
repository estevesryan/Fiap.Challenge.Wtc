namespace Fiap.Challenge.Wtc.Application.DTOs.Users;

public record GetUsersRequest
{
    public List<string>? Tags { get; init; }
    public int? Score { get; init; }
    public string? Status { get; init; }
}

public record GetUsersResponse
{
    public List<UserInfo> Users { get; init; } = new();
}

public record UserInfo
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Profile { get; init; } = string.Empty;
    public List<string> Tags { get; init; } = new();
    public int Score { get; init; }
    public string Status { get; init; } = string.Empty;
}