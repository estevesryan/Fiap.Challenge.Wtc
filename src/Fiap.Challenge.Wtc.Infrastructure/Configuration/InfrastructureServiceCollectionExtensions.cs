using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Challenge.Wtc.Infrastructure.Configuration;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Aqui você pode registrar os serviços de infraestrutura
        // Exemplo: services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}