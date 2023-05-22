namespace BuberDinner.Contracts.Authentication;

public record AuthenticationResponse(
    ulong id,
    string first_name,
    string last_name,
    string email,
    string token
);