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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
