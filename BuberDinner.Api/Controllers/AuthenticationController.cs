using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController: ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
            firstName: request.first_name,
            lastName: request.last_name,
            email: request.email,
            password: request.password
        );

        return authResult.Match(
            authResult => Ok(new AuthenticationResponse(
                id: authResult.User.Id,
                first_name: authResult.User.FirstName,
                last_name: authResult.User.LastName,
                email: authResult.User.Email,
                token: authResult.Token
            )),
            errors => Problem(errors)
        );
    }
    
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            email: request.email,
            password: request.password
        );

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return Ok(new AuthenticationResponse(
            id: authResult.Value.User.Id,
            first_name: authResult.Value.User.FirstName,
            last_name: authResult.Value.User.LastName,
            email: authResult.Value.User.Email,
            token: authResult.Value.Token
        ));
    }
}