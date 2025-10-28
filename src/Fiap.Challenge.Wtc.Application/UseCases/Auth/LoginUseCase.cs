using Fiap.Challenge.Wtc.Application.Common;
using Fiap.Challenge.Wtc.Application.DTOs.Auth;
using Fiap.Challenge.Wtc.Application.Interfaces;
using Fiap.Challenge.Wtc.Domain.Repositories;
using Fiap.Challenge.Wtc.Domain.ValueObjects;

namespace Fiap.Challenge.Wtc.Application.UseCases.Auth;

public class LoginUseCase : IUseCase<LoginRequest, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginResponse>> ExecuteAsync(LoginRequest request)
    {
        try
        {
            var email = Email.Create(request.Email);
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return Result<LoginResponse>.Failure("Invalid email or password");

            if (user.Profile != request.Profile)
                return Result<LoginResponse>.Failure("Invalid profile type for this user");

            if (!_passwordHasher.VerifyPassword(request.Senha, user.PasswordHash))
                return Result<LoginResponse>.Failure("Invalid email or password");

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Profile.ToString());

            var response = new LoginResponse
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Name = user.Name,
                    Profile = user.Profile.ToString(),
                    Tags = user.Tags,
                    Score = user.Score,
                    Status = user.Status.ToString()
                }
            };

            return Result<LoginResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<LoginResponse>.Failure($"Error during login: {ex.Message}");
        }
    }
}