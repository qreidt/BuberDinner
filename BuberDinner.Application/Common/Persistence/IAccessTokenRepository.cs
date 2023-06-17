using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Persistence;

public interface IAccessTokenRepository
{
    AccessToken? GetById(ulong id);
    AccessToken? GetByToken(string token);

    AccessToken Store(AccessToken token);
}