using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Notes;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Notes;

public class UpdateNoteUseCase : IUseCase<(Guid noteId, UpdateNoteRequest request), Result<NoteDto>>
{
    private readonly INoteRepository _noteRepository;

    public UpdateNoteUseCase(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<NoteDto>> ExecuteAsync((Guid noteId, UpdateNoteRequest request) parameters)
    {
        try
        {
            var (noteId, request) = parameters;

            var note = await _noteRepository.GetByIdAsync(noteId);
            if (note == null)
                return Result<NoteDto>.Failure("Note not found");

            note.UpdateContent(request.Content);
            await _noteRepository.UpdateAsync(note);

            var noteDto = new NoteDto
            {
                Id = note.Id.ToString(),
                UserId = note.UserId.ToString(),
                Content = note.Content,
                CreatedAt = note.CreatedAt
            };

            return Result<NoteDto>.Success(noteDto);
        }
        catch (Exception ex)
        {
            return Result<NoteDto>.Failure($"Error updating note: {ex.Message}");
        }
    }
}