using Fiap.Challenge.Wtc.Application.DTOs.Users;
using Fiap.Challenge.Wtc.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : BaseController
{
    private readonly GetUsersUseCase _getUsersUseCase;

    public UsersController(GetUsersUseCase getUsersUseCase)
    {
        _getUsersUseCase = getUsersUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] List<string>? tags, [FromQuery] int? score, [FromQuery] string? status)
    {
        var request = new GetUsersRequest
        {
            Tags = tags,
            Score = score,
            Status = status
        };

        var result = await _getUsersUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}