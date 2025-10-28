using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(T result)
    {
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    protected IActionResult HandleResult(bool success, string? errorMessage = null)
    {
        if (success)
        {
            return Ok();
        }

        return BadRequest(errorMessage ?? "An error occurred");
    }
}