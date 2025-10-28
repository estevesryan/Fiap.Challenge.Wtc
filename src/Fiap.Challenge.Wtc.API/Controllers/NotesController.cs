using Fiap.Challenge.Wtc.Application.DTOs.Notes;
using Fiap.Challenge.Wtc.Application.UseCases.Notes;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : BaseController
{
    private readonly GetNotesUseCase _getNotesUseCase;
    private readonly CreateNoteUseCase _createNoteUseCase;
    private readonly UpdateNoteUseCase _updateNoteUseCase;
    private readonly DeleteNoteUseCase _deleteNoteUseCase;

    public NotesController(
        GetNotesUseCase getNotesUseCase,
        CreateNoteUseCase createNoteUseCase,
        UpdateNoteUseCase updateNoteUseCase,
        DeleteNoteUseCase deleteNoteUseCase)
    {
        _getNotesUseCase = getNotesUseCase;
        _createNoteUseCase = createNoteUseCase;
        _updateNoteUseCase = updateNoteUseCase;
        _deleteNoteUseCase = deleteNoteUseCase;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetNotes(string userId)
    {
        if (!Guid.TryParse(userId, out var userGuid))
            return BadRequest(new { error = "Invalid user ID" });

        var result = await _getNotesUseCase.ExecuteAsync(userGuid);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateNote(string userId, [FromBody] CreateNoteRequest request)
    {
        if (!Guid.TryParse(userId, out var userGuid))
            return BadRequest(new { error = "Invalid user ID" });

        var result = await _createNoteUseCase.ExecuteAsync((userGuid, request));

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPut("{noteId}")]
    public async Task<IActionResult> UpdateNote(string noteId, [FromBody] UpdateNoteRequest request)
    {
        if (!Guid.TryParse(noteId, out var noteGuid))
            return BadRequest(new { error = "Invalid note ID" });

        var result = await _updateNoteUseCase.ExecuteAsync((noteGuid, request));

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpDelete("{noteId}")]
    public async Task<IActionResult> DeleteNote(string noteId)
    {
        if (!Guid.TryParse(noteId, out var noteGuid))
            return BadRequest(new { error = "Invalid note ID" });

        var result = await _deleteNoteUseCase.ExecuteAsync(noteGuid);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(new { message = "Note deleted successfully" });
    }
}