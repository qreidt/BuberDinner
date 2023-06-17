using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository: IUserRepository
{
    private static ulong _sequence;
    private static readonly List<User> Users = new();

    public User? GetById(ulong id)
    {
        return Users.FirstOrDefault(user => user.Id == id);
    }

    public User? GetByEmail(string email)
    {
        return Users.FirstOrDefault(user => user.Email == email);
    }

    public User Store(User user)
    {
        user.Id = ++_sequence;
        Users.Add(user);
        
        return user;
    }
}