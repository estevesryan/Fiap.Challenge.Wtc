using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Notes;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Notes;

public class CreateNoteUseCase : IUseCase<(Guid userId, CreateNoteRequest request), Result<NoteDto>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;

    public CreateNoteUseCase(INoteRepository noteRepository, IUserRepository userRepository)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<NoteDto>> ExecuteAsync((Guid userId, CreateNoteRequest request) parameters)
    {
        try
        {
            var (userId, request) = parameters;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Result<NoteDto>.Failure("User not found");

            var note = new Note(userId, request.Content);
            await _noteRepository.AddAsync(note);

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
            return Result<NoteDto>.Failure($"Error creating note: {ex.Message}");
        }
    }
}