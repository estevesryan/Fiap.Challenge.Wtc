using Fiap.Challenge.Wtc.Domain.Entities;

namespace Fiap.Challenge.Wtc.Domain.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    Task<IEnumerable<Message>> GetConversationAsync(Guid userId, Guid receiverId, int page = 1, int pageSize = 50);
    Task<IEnumerable<Message>> GetGroupMessagesAsync(Guid groupId, int page = 1, int pageSize = 50);
}