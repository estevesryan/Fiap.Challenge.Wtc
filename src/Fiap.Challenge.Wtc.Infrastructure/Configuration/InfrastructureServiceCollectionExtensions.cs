using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;
using Fiap.Challenge.Wtc.Infrastructure.Repositories;
using Fiap.Challenge.Wtc.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Challenge.Wtc.Infrastructure.Configuration;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Repositories
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<ISegmentRepository, SegmentRepository>();
        services.AddSingleton<ICampaignRepository, CampaignRepository>();
        services.AddSingleton<INoteRepository, NoteRepository>();
        services.AddSingleton<IGroupRepository, GroupRepository>();
        services.AddSingleton<IMessageRepository, MessageRepository>();

        // Services
        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}