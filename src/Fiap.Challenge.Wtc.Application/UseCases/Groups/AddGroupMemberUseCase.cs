using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Groups;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Groups;

public class AddGroupMemberUseCase : IUseCase<(Guid groupId, AddGroupMemberRequest request), Result>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public AddGroupMemberUseCase(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> ExecuteAsync((Guid groupId, AddGroupMemberRequest request) parameters)
    {
        try
        {
            var (groupId, request) = parameters;

            if (!Guid.TryParse(request.UserId, out var userId))
                return Result.Failure("Invalid user ID");

            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null)
                return Result.Failure("Group not found");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Result.Failure("User not found");

            group.AddMember(userId);
            await _groupRepository.UpdateAsync(group);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error adding member to group: {ex.Message}");
        }
    }
}