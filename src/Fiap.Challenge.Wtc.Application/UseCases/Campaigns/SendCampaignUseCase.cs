using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Campaigns;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Campaigns;

public class SendCampaignUseCase : IUseCase<SendCampaignRequest, Result>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ISegmentRepository _segmentRepository;

    public SendCampaignUseCase(
        ICampaignRepository campaignRepository,
        ISegmentRepository segmentRepository)
    {
        _campaignRepository = campaignRepository;
        _segmentRepository = segmentRepository;
    }

    public async Task<Result> ExecuteAsync(SendCampaignRequest request)
    {
        try
        {
            if (!Guid.TryParse(request.SegmentId, out var segmentId))
                return Result.Failure("Invalid segment ID");

            var segment = await _segmentRepository.GetByIdAsync(segmentId);
            if (segment == null)
                return Result.Failure("Segment not found");

            var actions = request.Message.Actions?
                .ToDictionary(a => a.Action, a => a.Title) ?? new Dictionary<string, string>();

            var campaign = new Campaign(
                segmentId,
                request.Message.Title,
                request.Message.Body,
                request.Message.Url,
                actions,
                request.Message.ActionUrls ?? new Dictionary<string, string>()
            );

            await _campaignRepository.AddAsync(campaign);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error sending campaign: {ex.Message}");
        }
    }
}