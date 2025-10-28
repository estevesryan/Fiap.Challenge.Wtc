namespace Fiap.Challenge.Wtc.Application.DTOs.Notes;

public record CreateNoteRequest
{
    public string Content { get; init; } = string.Empty;
}

public record UpdateNoteRequest
{
    public string Content { get; init; } = string.Empty;
}

public record GetNotesResponse
{
    public List<NoteDto> Notes { get; init; } = new();
}

public record NoteDto
{
    public string Id { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}