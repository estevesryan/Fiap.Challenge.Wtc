using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Infrastructure.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public Task<IEnumerable<Message>> GetConversationAsync(Guid userId, Guid receiverId, int page = 1, int pageSize = 50)
    {
        var messages = _entities
            .Where(m => !m.IsGroupMessage &&
                       ((m.SenderId == userId && m.ReceiverId == receiverId) ||
                        (m.SenderId == receiverId && m.ReceiverId == userId)))
            .OrderByDescending(m => m.SentAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(m => m.SentAt);

        return Task.FromResult<IEnumerable<Message>>(messages);
    }

    public Task<IEnumerable<Message>> GetGroupMessagesAsync(Guid groupId, int page = 1, int pageSize = 50)
    {
        var messages = _entities
            .Where(m => m.IsGroupMessage && m.GroupId == groupId)
            .OrderByDescending(m => m.SentAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(m => m.SentAt);

        return Task.FromResult<IEnumerable<Message>>(messages);
    }
}