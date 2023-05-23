using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController: ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }


    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await _mediator.Send(
            new RegisterCommand(
                FirstName: request.first_name,
                LastName: request.last_name,
                Email: request.email,
                Password: request.password
            )
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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await _mediator.Send(
            new LoginQuery(
                Email: request.email,
                Password: request.password
            )
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