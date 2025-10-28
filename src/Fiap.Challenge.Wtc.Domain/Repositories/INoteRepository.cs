using Fiap.Challenge.Wtc.Domain.Entities;

namespace Fiap.Challenge.Wtc.Domain.Repositories;

public interface INoteRepository : IRepository<Note>
{
    Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId);
}