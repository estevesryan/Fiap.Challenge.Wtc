namespace Fiap.Challenge.Wtc.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email, string profile);
    bool ValidateToken(string token);
    Guid? GetUserIdFromToken(string token);
}