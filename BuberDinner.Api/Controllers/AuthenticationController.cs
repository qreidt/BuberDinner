using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            firstName: request.first_name,
            lastName: request.last_name,
            email: request.email,
            password: request.password
        );

        return Ok(new AuthenticationResponse(
            id: authResult.User.Id,
            first_name: authResult.User.FirstName,
            last_name: authResult.User.LastName,
            email: authResult.User.Email,
            token: authResult.Token
        ));
    }
    
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            email: request.email,
            password: request.password
        );
        
        return Ok(new AuthenticationResponse(
            id: authResult.User.Id,
            first_name: authResult.User.FirstName,
            last_name: authResult.User.LastName,
            email: authResult.User.Email,
            token: authResult.Token
        ));
    }
}