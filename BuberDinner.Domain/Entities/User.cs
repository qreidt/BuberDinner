namespace BuberDinner.Domain.Entities;

public class User
{
    public ulong Id { get; set; } 

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}