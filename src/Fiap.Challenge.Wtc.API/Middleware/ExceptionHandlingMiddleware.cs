using System.Net;
using System.Text.Json;
using Fiap.Challenge.Wtc.Domain.Exceptions;

namespace Fiap.Challenge.Wtc.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            EntityNotFoundException ex => new ErrorResponse
            {
                Message = ex.Message,
                StatusCode = (int)HttpStatusCode.NotFound
            },
            BusinessRuleValidationException ex => new ErrorResponse
            {
                Message = ex.Message,
                StatusCode = (int)HttpStatusCode.BadRequest
            },
            InvalidValueObjectException ex => new ErrorResponse
            {
                Message = ex.Message,
                StatusCode = (int)HttpStatusCode.BadRequest
            },
            _ => new ErrorResponse
            {
                Message = "An error occurred while processing your request.",
                StatusCode = (int)HttpStatusCode.InternalServerError
            }
        };

        context.Response.StatusCode = response.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }

    private record ErrorResponse
    {
        public string Message { get; init; } = string.Empty;
        public int StatusCode { get; init; }
    }
}