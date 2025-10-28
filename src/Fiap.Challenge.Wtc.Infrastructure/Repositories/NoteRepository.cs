using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Infrastructure.Repositories;

public class NoteRepository : BaseRepository<Note>, INoteRepository
{
    public Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
    {
        var notes = _entities.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt);
        return Task.FromResult<IEnumerable<Note>>(notes);
    }
}