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
            first_name: request.first_name,
            last_name: request.last_name,
            email: request.email,
            password: request.password
        );

        return Ok(new AuthenticationResponse(
            id: authResult.id,
            first_name: authResult.first_name,
            last_name: authResult.last_name,
            email: authResult.email,
            token: authResult.token
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
            id: authResult.id,
            first_name: authResult.first_name,
            last_name: authResult.last_name,
            email: authResult.email,
            token: authResult.token
        ));
    }
}