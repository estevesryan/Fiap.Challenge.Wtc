using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly List<T> _entities = new();

    public virtual Task<T?> GetByIdAsync(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entity);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_entities.AsEnumerable());
    }

    public virtual Task<T> AddAsync(T entity)
    {
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<T> UpdateAsync(T entity)
    {
        var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
        if (existingEntity != null)
        {
            _entities.Remove(existingEntity);
            _entities.Add(entity);
            entity.UpdateTimestamp();
        }
        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity != null)
        {
            _entities.Remove(entity);
        }
        return Task.CompletedTask;
    }

    public virtual Task<bool> ExistsAsync(Guid id)
    {
        var exists = _entities.Any(e => e.Id == id);
        return Task.FromResult(exists);
    }
}