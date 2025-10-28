using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Chat;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Chat;

public class GetGroupMessagesUseCase : IUseCase<GetGroupMessagesRequest, Result<GetGroupMessagesResponse>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IGroupRepository _groupRepository;

    public GetGroupMessagesUseCase(
        IMessageRepository messageRepository,
        IGroupRepository groupRepository)
    {
        _messageRepository = messageRepository;
        _groupRepository = groupRepository;
    }

    public async Task<Result<GetGroupMessagesResponse>> ExecuteAsync(GetGroupMessagesRequest request)
    {
        try
        {
            if (!Guid.TryParse(request.GroupId, out var groupId))
                return Result<GetGroupMessagesResponse>.Failure("Invalid group ID");

            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null)
                return Result<GetGroupMessagesResponse>.Failure("Group not found");

            var messages = await _messageRepository.GetGroupMessagesAsync(
                groupId,
                request.Page,
                request.PageSize
            );

            var response = new GetGroupMessagesResponse
            {
                Messages = messages.Select(m => new GroupMessageDto
                {
                    SenderId = m.SenderId.ToString(),
                    GroupName = group.Name,
                    Message = m.Content,
                    Timestamp = m.SentAt
                }).ToList()
            };

            return Result<GetGroupMessagesResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetGroupMessagesResponse>.Failure($"Error fetching group messages: {ex.Message}");
        }
    }
}