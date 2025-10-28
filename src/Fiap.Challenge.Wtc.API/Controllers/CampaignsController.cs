using Fiap.Challenge.Wtc.Application.DTOs.Campaigns;
using Fiap.Challenge.Wtc.Application.UseCases.Campaigns;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
public class CampaignsController : BaseController
{
    private readonly SendCampaignUseCase _sendCampaignUseCase;
    private readonly GetCampaignsUseCase _getCampaignsUseCase;

    public CampaignsController(
        SendCampaignUseCase sendCampaignUseCase,
        GetCampaignsUseCase getCampaignsUseCase)
    {
        _sendCampaignUseCase = sendCampaignUseCase;
        _getCampaignsUseCase = getCampaignsUseCase;
    }

    [HttpPost("send-campaigns")]
    public async Task<IActionResult> SendCampaign([FromBody] SendCampaignRequest request)
    {
        var result = await _sendCampaignUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(new { message = "Campaign sent successfully" });
    }

    [HttpGet("campaigns")]
    public async Task<IActionResult> GetCampaigns()
    {
        var result = await _getCampaignsUseCase.ExecuteAsync();

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}