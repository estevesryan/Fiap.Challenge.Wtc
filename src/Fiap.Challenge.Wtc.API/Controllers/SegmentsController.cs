using Fiap.Challenge.Wtc.Application.UseCases.Segments;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SegmentsController : BaseController
{
    private readonly GetSegmentsUseCase _getSegmentsUseCase;

    public SegmentsController(GetSegmentsUseCase getSegmentsUseCase)
    {
        _getSegmentsUseCase = getSegmentsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetSegments()
    {
        var result = await _getSegmentsUseCase.ExecuteAsync();
        return Ok(result);
    }
}