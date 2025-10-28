using Fiap.Challenge.Wtc.Application.DTOs.Chat;
using Fiap.Challenge.Wtc.Application.UseCases.Chat;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
public class ChatController : BaseController
{
    private readonly SendMessageUseCase _sendMessageUseCase;
    private readonly GetMessagesUseCase _getMessagesUseCase;
    private readonly SendGroupMessageUseCase _sendGroupMessageUseCase;
    private readonly GetGroupMessagesUseCase _getGroupMessagesUseCase;

    public ChatController(
        SendMessageUseCase sendMessageUseCase,
        GetMessagesUseCase getMessagesUseCase,
        SendGroupMessageUseCase sendGroupMessageUseCase,
        GetGroupMessagesUseCase getGroupMessagesUseCase)
    {
        _sendMessageUseCase = sendMessageUseCase;
        _getMessagesUseCase = getMessagesUseCase;
        _sendGroupMessageUseCase = sendGroupMessageUseCase;
        _getGroupMessagesUseCase = getGroupMessagesUseCase;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        var result = await _sendMessageUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(new { message = "Message sent successfully" });
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages([FromQuery] GetMessagesRequest request)
    {
        var result = await _getMessagesUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost("send-group-message")]
    public async Task<IActionResult> SendGroupMessage([FromBody] SendGroupMessageRequest request)
    {
        var result = await _sendGroupMessageUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(new { message = "Group message sent successfully" });
    }

    [HttpGet("group-messages")]
    public async Task<IActionResult> GetGroupMessages([FromQuery] GetGroupMessagesRequest request)
    {
        var result = await _getGroupMessagesUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}