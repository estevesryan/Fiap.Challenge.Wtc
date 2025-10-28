namespace Fiap.Challenge.Wtc.Application.DTOs;

public record BaseDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}