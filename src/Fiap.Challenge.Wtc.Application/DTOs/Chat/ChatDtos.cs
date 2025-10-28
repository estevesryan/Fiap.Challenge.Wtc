namespace Fiap.Challenge.Wtc.Application.DTOs.Chat;

public record SendMessageRequest
{
    public string SenderId { get; init; } = string.Empty;
    public string ReceiverId { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

public record SendGroupMessageRequest
{
    public string SenderId { get; init; } = string.Empty;
    public string GroupId { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

public record GetMessagesRequest
{
    public string UserId { get; init; } = string.Empty;
    public string ReceiverId { get; init; } = string.Empty;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

public record GetGroupMessagesRequest
{
    public string GroupId { get; init; } = string.Empty;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

public record GetMessagesResponse
{
    public List<MessageDetailDto> Messages { get; init; } = new();
}

public record MessageDetailDto
{
    public string SenderId { get; init; } = string.Empty;
    public string? ReceiverId { get; init; }
    public string? GroupId { get; init; }
    public string Message { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
}

public record GetGroupMessagesResponse
{
    public List<GroupMessageDto> Messages { get; init; } = new();
}

public record GroupMessageDto
{
    public string SenderId { get; init; } = string.Empty;
    public string GroupName { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
}