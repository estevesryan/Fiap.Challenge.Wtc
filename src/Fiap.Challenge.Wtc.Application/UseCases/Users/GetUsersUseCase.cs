using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Users;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Enums;
using Fiap.Challenge.Wtc.Domain.Repositories;

namespace Fiap.Challenge.Wtc.Application.UseCases.Users;

public class GetUsersUseCase : IUseCase<GetUsersRequest, Result<GetUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetUsersResponse>> ExecuteAsync(GetUsersRequest request)
    {
        try
        {
            UserStatus? status = null;
            if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<UserStatus>(request.Status, true, out var parsedStatus))
            {
                status = parsedStatus;
            }

            var users = await _userRepository.GetByFiltersAsync(
                request.Tags,
                request.Score,
                status
            );

            var response = new GetUsersResponse
            {
                Users = users.Select(u => new UserInfo
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    Name = u.Name,
                    Profile = u.Profile.ToString(),
                    Tags = u.Tags,
                    Score = u.Score,
                    Status = u.Status.ToString()
                }).ToList()
            };

            return Result<GetUsersResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetUsersResponse>.Failure($"Error fetching users: {ex.Message}");
        }
    }
}