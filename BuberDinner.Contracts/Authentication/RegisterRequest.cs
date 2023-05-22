namespace BuberDinner.Contracts.Authentication;

public record RegisterRequest(
    string first_name,
    string last_name,
    string email,
    string password
);