namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(
        string first_name,
        string last_name,
        string email,
        string password
    );
    
    AuthenticationResult Login(
        string email,
        string password
    );
}