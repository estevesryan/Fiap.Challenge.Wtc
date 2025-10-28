namespace Fiap.Challenge.Wtc.Domain.Entities;

public class Message : BaseEntity
{
    public Guid SenderId { get; private set; }
    public Guid? ReceiverId { get; private set; }
    public Guid? GroupId { get; private set; }
    public string Content { get; private set; }
    public DateTime SentAt { get; private set; }
    public bool IsGroupMessage => GroupId.HasValue;

    private Message() { }

    // Mensagem individual (1:1)
    public Message(Guid senderId, Guid receiverId, string content)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content ?? throw new ArgumentNullException(nameof(content));
        SentAt = DateTime.UtcNow;
    }

    // Mensagem de grupo
    public static Message CreateGroupMessage(Guid senderId, Guid groupId, string content)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            SenderId = senderId,
            GroupId = groupId,
            Content = content ?? throw new ArgumentNullException(nameof(content)),
            SentAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}