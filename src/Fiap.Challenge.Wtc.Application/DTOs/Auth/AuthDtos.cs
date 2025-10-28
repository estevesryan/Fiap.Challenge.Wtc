using Fiap.Challenge.Wtc.Domain.Enums;

namespace Fiap.Challenge.Wtc.Application.DTOs.Auth;

public record LoginRequest
{
    public string Email { get; init; } = string.Empty;
    public string Senha { get; init; } = string.Empty;
    public ProfileType Profile { get; init; }
}

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public UserDto User { get; init; } = null!;
}

public record UserDto
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Profile { get; init; } = string.Empty;
    public List<string> Tags { get; init; } = new();
    public int Score { get; init; }
    public string Status { get; init; } = string.Empty;
}