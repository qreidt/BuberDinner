using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class AccessTokenRepository: IAccessTokenRepository
{
    private static ulong _sequence;
    private static readonly List<AccessToken> AccessTokens = new();
    
    public AccessToken? GetById(ulong id)
    {
        return AccessTokens.FirstOrDefault(accessToken => accessToken.Id == id);
    }

    public AccessToken? GetByToken(string token)
    {
        return AccessTokens.FirstOrDefault(accessToken => accessToken.Token == token);
    }

    public AccessToken Store(AccessToken token)
    {
        token.Id = ++_sequence;
        AccessTokens.Add(token);

        return token;
    }
}