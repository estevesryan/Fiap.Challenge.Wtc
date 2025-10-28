using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Chat;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Chat;

public class SendGroupMessageUseCase : IUseCase<SendGroupMessageRequest, Result>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public SendGroupMessageUseCase(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IGroupRepository groupRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<Result> ExecuteAsync(SendGroupMessageRequest request)
    {
        try
        {
            if (!Guid.TryParse(request.SenderId, out var senderId))
                return Result.Failure("Invalid sender ID");

            if (!Guid.TryParse(request.GroupId, out var groupId))
                return Result.Failure("Invalid group ID");

            var sender = await _userRepository.GetByIdAsync(senderId);
            if (sender == null)
                return Result.Failure("Sender not found");

            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null)
                return Result.Failure("Group not found");

            if (!group.MemberIds.Contains(senderId))
                return Result.Failure("Sender is not a member of this group");

            var message = Message.CreateGroupMessage(senderId, groupId, request.Message);
            await _messageRepository.AddAsync(message);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error sending group message: {ex.Message}");
        }
    }
}