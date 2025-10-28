using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Groups;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Entities;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Groups;

public class CreateGroupUseCase : IUseCase<CreateGroupRequest, Result<GroupDto>>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupUseCase(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GroupDto>> ExecuteAsync(CreateGroupRequest request)
    {
        try
        {
            var group = new Group(request.Name, request.Description);
            await _groupRepository.AddAsync(group);

            var groupDto = new GroupDto
            {
                Id = group.Id.ToString(),
                Name = group.Name,
                Description = group.Description,
                MemberIds = group.MemberIds.Select(m => m.ToString()).ToList(),
                CreatedAt = group.CreatedAt
            };

            return Result<GroupDto>.Success(groupDto);
        }
        catch (Exception ex)
        {
            return Result<GroupDto>.Failure($"Error creating group: {ex.Message}");
        }
    }
}