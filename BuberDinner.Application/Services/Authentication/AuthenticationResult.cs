namespace BuberDinner.Application.Services.Authentication;

public record AuthenticationResult(
    ulong id,
    string first_name,
    string last_name,
    string email,
    string token
);