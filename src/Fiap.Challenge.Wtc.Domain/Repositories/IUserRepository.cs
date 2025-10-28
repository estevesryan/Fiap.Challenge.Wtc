using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Enums;
using Fiap.Challenge.Wtc.Domain.ValueObjects;

namespace Fiap.Challenge.Wtc.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(Email email);
    Task<IEnumerable<User>> GetByFiltersAsync(List<string>? tags = null, int? score = null, UserStatus? status = null);
}