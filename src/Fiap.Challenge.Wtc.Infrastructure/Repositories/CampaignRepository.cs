using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Infrastructure.Repositories;

public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
{
    public Task<IEnumerable<Campaign>> GetBySegmentIdAsync(Guid segmentId)
    {
        var campaigns = _entities.Where(c => c.SegmentId == segmentId);
        return Task.FromResult<IEnumerable<Campaign>>(campaigns);
    }
}