namespace Fiap.Challenge.Wtc.Domain.Entities;

public class Segment : BaseEntity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public List<string> Tags { get; private set; }
    public int? MinScore { get; private set; }
    public int? MaxScore { get; private set; }

    private Segment() { }

    public Segment(string title, string? description = null)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        Tags = new List<string>();
    }

    public void UpdateTitle(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        UpdateTimestamp();
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
        UpdateTimestamp();
    }

    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag))
            Tags.Add(tag);
        
        UpdateTimestamp();
    }

    public void SetScoreRange(int? minScore, int? maxScore)
    {
        MinScore = minScore;
        MaxScore = maxScore;
        UpdateTimestamp();
    }
}