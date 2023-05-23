using ErrorOr;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Errors;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService: IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // Validate e-mail doesn't exist
        if (_userRepository.GetByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // Create and Persist User
        var user = _userRepository.Store(new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        });
        
        // Generate Token
        var token = _tokenGenerator.GenerateToken(user.Id);
        
        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Validate User Exists
        if (_userRepository.GetByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Check if password is correct
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Create Token
        var token = _tokenGenerator.GenerateToken(0);
        
        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }
}