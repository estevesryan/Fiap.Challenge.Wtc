using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Enums;
using Fiap.Challenge.Wtc.Domain.Repositories;
using Fiap.Challenge.Wtc.Domain.ValueObjects;

namespace Fiap.Challenge.Wtc.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(
        IUserRepository userRepository,
        ISegmentRepository segmentRepository,
        IGroupRepository groupRepository,
        IPasswordHasher passwordHasher)
    {
        // Seed Users
        await SeedUsersAsync(userRepository, passwordHasher);
        
        // Seed Segments
        await SeedSegmentsAsync(segmentRepository);
        
        // Seed Groups
        await SeedGroupsAsync(groupRepository);
    }

    private static async Task SeedUsersAsync(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        var users = await userRepository.GetAllAsync();
        if (users.Any()) return;

        var clientUser = new User(
            Email.Create("client@test.com"),
            "Cliente Teste",
            passwordHasher.HashPassword("senha123"),
            ProfileType.Client
        );
        clientUser.AddTag("vip");
        clientUser.AddTag("premium");
        clientUser.UpdateScore(100);

        var operatorUser = new User(
            Email.Create("operator@test.com"),
            "Operador Teste",
            passwordHasher.HashPassword("senha123"),
            ProfileType.Operator
        );

        var client2 = new User(
            Email.Create("client2@test.com"),
            "Cliente Dois",
            passwordHasher.HashPassword("senha123"),
            ProfileType.Client
        );
        client2.AddTag("basic");
        client2.UpdateScore(50);

        await userRepository.AddAsync(clientUser);
        await userRepository.AddAsync(operatorUser);
        await userRepository.AddAsync(client2);
    }

    private static async Task SeedSegmentsAsync(ISegmentRepository segmentRepository)
    {
        var segments = await segmentRepository.GetAllAsync();
        if (segments.Any()) return;

        var vipSegment = new Segment("VIP Customers", "Segment for VIP customers");
        vipSegment.AddTag("vip");
        vipSegment.SetScoreRange(80, null);

        var basicSegment = new Segment("Basic Customers", "Segment for basic customers");
        basicSegment.AddTag("basic");
        basicSegment.SetScoreRange(0, 79);

        var premiumSegment = new Segment("Premium Members", "Segment for premium members");
        premiumSegment.AddTag("premium");
        premiumSegment.SetScoreRange(100, null);

        await segmentRepository.AddAsync(vipSegment);
        await segmentRepository.AddAsync(basicSegment);
        await segmentRepository.AddAsync(premiumSegment);
    }

    private static async Task SeedGroupsAsync(IGroupRepository groupRepository)
    {
        var groups = await groupRepository.GetAllAsync();
        if (groups.Any()) return;

        var group1 = new Group("General Discussion", "General chat for all members");
        var group2 = new Group("VIP Lounge", "Exclusive VIP chat");
        var group3 = new Group("Support", "Customer support group");

        await groupRepository.AddAsync(group1);
        await groupRepository.AddAsync(group2);
        await groupRepository.AddAsync(group3);
    }
}