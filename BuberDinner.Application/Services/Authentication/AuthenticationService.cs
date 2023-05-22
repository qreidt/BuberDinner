namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService: IAuthenticationService
{
    public AuthenticationResult Register(string first_name, string last_name, string email, string password)
    {
        return new AuthenticationResult(
            id: 0,
            first_name: first_name,
            last_name: last_name,
            email: email,
            token: "token"
        );
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            id: 0,
            first_name: "first_name",
            last_name: "last_name",
            email: email,
            token: "token"
        );
    }
}