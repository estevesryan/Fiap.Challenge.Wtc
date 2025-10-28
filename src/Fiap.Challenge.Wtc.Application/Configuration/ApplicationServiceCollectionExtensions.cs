using Fiap.Challenge.Wtc.Application.UseCases.Auth;
using Fiap.Challenge.Wtc.Application.UseCases.Campaigns;
using Fiap.Challenge.Wtc.Application.UseCases.Chat;
using Fiap.Challenge.Wtc.Application.UseCases.Groups;
using Fiap.Challenge.Wtc.Application.UseCases.Notes;
using Fiap.Challenge.Wtc.Application.UseCases.Segments;
using Fiap.Challenge.Wtc.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Challenge.Wtc.Application.Configuration;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Auth Use Cases
        services.AddScoped<LoginUseCase>();

        // User Use Cases
        services.AddScoped<GetUsersUseCase>();

        // Segment Use Cases
        services.AddScoped<GetSegmentsUseCase>();

        // Campaign Use Cases
        services.AddScoped<SendCampaignUseCase>();
        services.AddScoped<GetCampaignsUseCase>();

        // Note Use Cases
        services.AddScoped<GetNotesUseCase>();
        services.AddScoped<CreateNoteUseCase>();
        services.AddScoped<UpdateNoteUseCase>();
        services.AddScoped<DeleteNoteUseCase>();

        // Group Use Cases
        services.AddScoped<GetGroupsUseCase>();
        services.AddScoped<CreateGroupUseCase>();
        services.AddScoped<AddGroupMemberUseCase>();

        // Chat Use Cases
        services.AddScoped<SendMessageUseCase>();
        services.AddScoped<GetMessagesUseCase>();
        services.AddScoped<SendGroupMessageUseCase>();
        services.AddScoped<GetGroupMessagesUseCase>();

        return services;
    }
}