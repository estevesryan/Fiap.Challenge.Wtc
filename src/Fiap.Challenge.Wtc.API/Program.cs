using Fiap.Challenge.Wtc.API.Configuration;
using Fiap.Challenge.Wtc.API.Middleware;
using Fiap.Challenge.Wtc.Application.Configuration;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;
using Fiap.Challenge.Wtc.Infrastructure.Configuration;
using Fiap.Challenge.Wtc.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userRepository = services.GetRequiredService<IUserRepository>();
    var segmentRepository = services.GetRequiredService<ISegmentRepository>();
    var groupRepository = services.GetRequiredService<IGroupRepository>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher>();
    
    await DataSeeder.SeedAsync(userRepository, segmentRepository, groupRepository, passwordHasher);
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Habilitar Swagger em todos os ambientes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WTC API v1");
    c.RoutePrefix = string.Empty; // Swagger na raiz do site
    c.DocumentTitle = "WTC API - FIAP Challenge";
});

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new 
{ 
    status = "healthy", 
    timestamp = DateTime.UtcNow,
    version = "1.0.0"
})).ExcludeFromDescription();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
