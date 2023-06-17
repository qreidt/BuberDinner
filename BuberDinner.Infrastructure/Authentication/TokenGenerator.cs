using System.Security.Cryptography;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Authentication;

public class TokenGenerator: ITokenGenerator
{

    private readonly IAccessTokenRepository _tokenRepository;

    public TokenGenerator(IAccessTokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public string GenerateToken(ulong userId)
    {
        var plainTextToken = Guid.NewGuid().ToString();
        
        var token = Convert.ToBase64String(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(plainTextToken)
            )
        );

        _tokenRepository.Store(new AccessToken
        {
            Token = token,
            TokenableId = userId,
            TokenableEntity = "BuberDinner.Domain.Entities.User",
        });

        return plainTextToken;
    }
}