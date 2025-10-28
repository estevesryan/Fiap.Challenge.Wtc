namespace Fiap.Challenge.Wtc.Application.DTOs.Segments;

public record SegmentDto
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
}

public record GetSegmentsResponse
{
    public List<SegmentDto> Segments { get; init; } = new();
}