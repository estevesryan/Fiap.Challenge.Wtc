namespace Fiap.Challenge.Wtc.Domain.Entities;

public class Note : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Content { get; private set; }

    private Note() { }

    public Note(Guid userId, string content)
    {
        UserId = userId;
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public void UpdateContent(string content)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        UpdateTimestamp();
    }
}