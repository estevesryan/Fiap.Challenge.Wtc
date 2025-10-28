using Fiap.Challenge.Wtc.Domain.Entities;

namespace Fiap.Challenge.Wtc.Domain.Repositories;

public interface ICampaignRepository : IRepository<Campaign>
{
    Task<IEnumerable<Campaign>> GetBySegmentIdAsync(Guid segmentId);
}