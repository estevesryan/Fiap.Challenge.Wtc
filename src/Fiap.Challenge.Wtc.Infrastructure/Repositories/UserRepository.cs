using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Enums;
using Fiap.Challenge.Wtc.Domain.Repositories;
using Fiap.Challenge.Wtc.Domain.ValueObjects;

namespace Fiap.Challenge.Wtc.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public Task<User?> GetByEmailAsync(Email email)
    {
        var user = _entities.FirstOrDefault(u => u.Email.Value == email.Value);
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetByFiltersAsync(
        List<string>? tags = null,
        int? score = null,
        UserStatus? status = null)
    {
        var query = _entities.AsEnumerable();

        if (tags != null && tags.Any())
        {
            query = query.Where(u => tags.All(tag => u.Tags.Contains(tag)));
        }

        if (score.HasValue)
        {
            query = query.Where(u => u.Score == score.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(u => u.Status == status.Value);
        }

        return Task.FromResult(query);
    }
}