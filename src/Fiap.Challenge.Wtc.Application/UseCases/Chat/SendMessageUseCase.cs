using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Chat;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Chat;

public class SendMessageUseCase : IUseCase<SendMessageRequest, Result>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public SendMessageUseCase(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> ExecuteAsync(SendMessageRequest request)
    {
        try
        {
            if (!Guid.TryParse(request.SenderId, out var senderId))
                return Result.Failure("Invalid sender ID");

            if (!Guid.TryParse(request.ReceiverId, out var receiverId))
                return Result.Failure("Invalid receiver ID");

            var sender = await _userRepository.GetByIdAsync(senderId);
            if (sender == null)
                return Result.Failure("Sender not found");

            var receiver = await _userRepository.GetByIdAsync(receiverId);
            if (receiver == null)
                return Result.Failure("Receiver not found");

            var message = new Message(senderId, receiverId, request.Message);
            await _messageRepository.AddAsync(message);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error sending message: {ex.Message}");
        }
    }
}