namespace BuberDinner.Domain.Entities;

public class AccessToken
{
    public ulong Id { get; set; }
    public string TokenableEntity { get; set; } = null!;
    public ulong TokenableId { get; set; }
    public string Token { get; set; } = null!;
}