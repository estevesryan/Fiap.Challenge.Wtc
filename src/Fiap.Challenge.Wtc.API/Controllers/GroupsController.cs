using Fiap.Challenge.Wtc.Application.DTOs.Groups;
using Fiap.Challenge.Wtc.Application.UseCases.Groups;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Challenge.Wtc.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : BaseController
{
    private readonly GetGroupsUseCase _getGroupsUseCase;
    private readonly CreateGroupUseCase _createGroupUseCase;
    private readonly AddGroupMemberUseCase _addGroupMemberUseCase;

    public GroupsController(
        GetGroupsUseCase getGroupsUseCase,
        CreateGroupUseCase createGroupUseCase,
        AddGroupMemberUseCase addGroupMemberUseCase)
    {
        _getGroupsUseCase = getGroupsUseCase;
        _createGroupUseCase = createGroupUseCase;
        _addGroupMemberUseCase = addGroupMemberUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var result = await _getGroupsUseCase.ExecuteAsync();

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
    {
        var result = await _createGroupUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost("{groupId}/members")]
    public async Task<IActionResult> AddMember(string groupId, [FromBody] AddGroupMemberRequest request)
    {
        if (!Guid.TryParse(groupId, out var groupGuid))
            return BadRequest(new { error = "Invalid group ID" });

        var result = await _addGroupMemberUseCase.ExecuteAsync((groupGuid, request));

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(new { message = "Member added successfully" });
    }
}