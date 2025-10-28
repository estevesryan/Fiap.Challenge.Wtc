using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Notes;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Notes;

public class GetNotesUseCase : IUseCase<Guid, Result<GetNotesResponse>>
{
    private readonly INoteRepository _noteRepository;

    public GetNotesUseCase(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<GetNotesResponse>> ExecuteAsync(Guid userId)
    {
        try
        {
            var notes = await _noteRepository.GetByUserIdAsync(userId);

            var response = new GetNotesResponse
            {
                Notes = notes.Select(n => new NoteDto
                {
                    Id = n.Id.ToString(),
                    UserId = n.UserId.ToString(),
                    Content = n.Content,
                    CreatedAt = n.CreatedAt
                }).ToList()
            };

            return Result<GetNotesResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetNotesResponse>.Failure($"Error fetching notes: {ex.Message}");
        }
    }
}