namespace Fiap.Challenge.Wtc.Application.DTOs.Campaigns;

public record SendCampaignRequest
{
    public string SegmentId { get; init; } = string.Empty;
    public MessageDto Message { get; init; } = null!;
}

public record MessageDto
{
    public string Title { get; init; } = string.Empty;
    public string Body { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public List<ActionDto>? Actions { get; init; }
    public Dictionary<string, string>? ActionUrls { get; init; }
}

public record ActionDto
{
    public string Action { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
}

public record GetCampaignsResponse
{
    public List<CampaignDto> Campaigns { get; init; } = new();
}

public record CampaignDto
{
    public string Id { get; init; } = string.Empty;
    public string SegmentId { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Body { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public DateTime SentAt { get; init; }
}