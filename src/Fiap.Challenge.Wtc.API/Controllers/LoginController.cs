using Fiap.Challenge.Wtc.API.Controllers;
using Fiap.Challenge.Wtc.Application.DTOs.Auth;
using Fiap.Challenge.Wtc.Application.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : BaseController
{
    private readonly LoginUseCase _loginUseCase;

    public LoginController(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}