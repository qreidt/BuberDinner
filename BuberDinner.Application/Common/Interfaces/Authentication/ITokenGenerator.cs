namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface ITokenGenerator
{
    string GenerateToken(ulong userId);
}