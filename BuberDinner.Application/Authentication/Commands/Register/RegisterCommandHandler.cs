using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Errors;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        // Validate e-mail doesn't exist
        if (_userRepository.GetByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // Create and Persist User
        var user = _userRepository.Store(new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        });
        
        // Generate AccessToken
        var token = _tokenGenerator.GenerateToken(user.Id);
        
        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }
}