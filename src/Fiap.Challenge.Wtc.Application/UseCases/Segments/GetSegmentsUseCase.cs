using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Segments;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Segments;

public class GetSegmentsUseCase : IUseCase<GetSegmentsResponse>
{
    private readonly ISegmentRepository _segmentRepository;

    public GetSegmentsUseCase(ISegmentRepository segmentRepository)
    {
        _segmentRepository = segmentRepository;
    }

    public async Task<GetSegmentsResponse> ExecuteAsync()
    {
        var segments = await _segmentRepository.GetAllAsync();

        return new GetSegmentsResponse
        {
            Segments = segments.Select(s => new SegmentDto
            {
                Id = s.Id.ToString(),
                Title = s.Title
            }).ToList()
        };
    }
}