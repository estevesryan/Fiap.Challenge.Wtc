using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Groups;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Groups;

public class GetGroupsUseCase : IUseCase<Result<GetGroupsResponse>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupsUseCase(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GetGroupsResponse>> ExecuteAsync()
    {
        try
        {
            var groups = await _groupRepository.GetAllAsync();

            var response = new GetGroupsResponse
            {
                Groups = groups.Select(g => new GroupDto
                {
                    Id = g.Id.ToString(),
                    Name = g.Name,
                    Description = g.Description,
                    MemberIds = g.MemberIds.Select(m => m.ToString()).ToList(),
                    CreatedAt = g.CreatedAt
                }).ToList()
            };

            return Result<GetGroupsResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetGroupsResponse>.Failure($"Error fetching groups: {ex.Message}");
        }
    }
}