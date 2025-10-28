using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Campaigns;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Campaigns;

public class GetCampaignsUseCase : IUseCase<Result<GetCampaignsResponse>>
{
    private readonly ICampaignRepository _campaignRepository;

    public GetCampaignsUseCase(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<Result<GetCampaignsResponse>> ExecuteAsync()
    {
        try
        {
            var campaigns = await _campaignRepository.GetAllAsync();

            var response = new GetCampaignsResponse
            {
                Campaigns = campaigns.Select(c => new CampaignDto
                {
                    Id = c.Id.ToString(),
                    SegmentId = c.SegmentId.ToString(),
                    Title = c.Title,
                    Body = c.Body,
                    Url = c.Url,
                    SentAt = c.SentAt
                }).ToList()
            };

            return Result<GetCampaignsResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetCampaignsResponse>.Failure($"Error fetching campaigns: {ex.Message}");
        }
    }
}