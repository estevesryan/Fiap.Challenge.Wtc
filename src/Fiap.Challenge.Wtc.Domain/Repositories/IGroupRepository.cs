using Fiap.Challenge.Wtc.Domain.Entities;

namespace Fiap.Challenge.Wtc.Domain.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Task<IEnumerable<Group>> GetByMemberIdAsync(Guid userId);
}