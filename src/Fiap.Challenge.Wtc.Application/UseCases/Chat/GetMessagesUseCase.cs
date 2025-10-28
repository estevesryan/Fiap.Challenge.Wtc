using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Chat;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Chat;

public class GetMessagesUseCase : IUseCase<GetMessagesRequest, Result<GetMessagesResponse>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesUseCase(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<GetMessagesResponse>> ExecuteAsync(GetMessagesRequest request)
    {
        try
        {
            if (!Guid.TryParse(request.UserId, out var userId))
                return Result<GetMessagesResponse>.Failure("Invalid user ID");

            if (!Guid.TryParse(request.ReceiverId, out var receiverId))
                return Result<GetMessagesResponse>.Failure("Invalid receiver ID");

            var messages = await _messageRepository.GetConversationAsync(
                userId,
                receiverId,
                request.Page,
                request.PageSize
            );

            var response = new GetMessagesResponse
            {
                Messages = messages.Select(m => new MessageDetailDto
                {
                    SenderId = m.SenderId.ToString(),
                    ReceiverId = m.ReceiverId?.ToString(),
                    GroupId = m.GroupId?.ToString(),
                    Message = m.Content,
                    Timestamp = m.SentAt
                }).ToList()
            };

            return Result<GetMessagesResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetMessagesResponse>.Failure($"Error fetching messages: {ex.Message}");
        }
    }
}