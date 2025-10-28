using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Infrastructure.Repositories;

public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public Task<IEnumerable<Group>> GetByMemberIdAsync(Guid userId)
    {
        var groups = _entities.Where(g => g.MemberIds.Contains(userId));
        return Task.FromResult<IEnumerable<Group>>(groups);
    }
}