using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Notes;

public class DeleteNoteUseCase : IUseCase<Guid, Result>
{
    private readonly INoteRepository _noteRepository;

    public DeleteNoteUseCase(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result> ExecuteAsync(Guid noteId)
    {
        try
        {
            var note = await _noteRepository.GetByIdAsync(noteId);
            if (note == null)
                return Result.Failure("Note not found");

            await _noteRepository.DeleteAsync(noteId);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error deleting note: {ex.Message}");
        }
    }
}