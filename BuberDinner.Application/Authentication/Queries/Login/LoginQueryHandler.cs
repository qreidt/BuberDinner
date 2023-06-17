using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Errors;
using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        // Validate User Exists
        if (_userRepository.GetByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Check if password is correct
        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Create AccessToken
        var token = _tokenGenerator.GenerateToken(user.Id);
        
        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }
}