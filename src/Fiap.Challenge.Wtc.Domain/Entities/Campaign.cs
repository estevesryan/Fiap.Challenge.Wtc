namespace Fiap.Challenge.Wtc.Domain.Entities;

public class Campaign : BaseEntity
{
    public Guid SegmentId { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public string Url { get; private set; }
    public Dictionary<string, string> Actions { get; private set; }
    public Dictionary<string, string> ActionUrls { get; private set; }
    public DateTime SentAt { get; private set; }

    private Campaign() { }

    public Campaign(
        Guid segmentId,
        string title,
        string body,
        string url,
        Dictionary<string, string> actions,
        Dictionary<string, string> actionUrls)
    {
        SegmentId = segmentId;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Body = body ?? throw new ArgumentNullException(nameof(body));
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Actions = actions ?? new Dictionary<string, string>();
        ActionUrls = actionUrls ?? new Dictionary<string, string>();
        SentAt = DateTime.UtcNow;
    }
}