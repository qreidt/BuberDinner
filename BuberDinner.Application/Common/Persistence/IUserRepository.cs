using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Persistence;

public interface IUserRepository
{
    User? GetByEmail(string email);

    User Store(User user);
}