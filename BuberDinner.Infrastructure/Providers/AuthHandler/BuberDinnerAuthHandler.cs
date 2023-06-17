using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace BuberDinner.Infrastructure.Providers.AuthHandler;

public class BuberDinnerAuthHandler: AuthenticationHandler<BuberDinnerAuthSchemeOptions>
{
    private readonly IAccessTokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    
    public BuberDinnerAuthHandler(
        IOptionsMonitor<BuberDinnerAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAccessTokenRepository tokenRepository,
        IUserRepository userRepository
    ) : base(options, logger, encoder, clock)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (! Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return Task.FromResult(AuthenticateResult.Fail("Header Not Found"));
        }

        var header = Request.Headers[HeaderNames.Authorization].ToString();
        var tokenMatch = Regex.Match(header, $"Bearer (?<token>.*)");
        
        // Fail
        if (!tokenMatch.Success)
        {
            return Task.FromResult(AuthenticateResult.Fail("Authentication Header is Empty"));
        }

        var token = tokenMatch.Groups["token"].Value;

        // Hash token with sha256
        var parsedToken = Convert.ToBase64String(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(token)
            )
        );

        var accessToken = _tokenRepository.GetByToken(parsedToken);
        if (
            accessToken is null
            || _userRepository.GetById(accessToken.TokenableId) is not User user
        )
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
        }


        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.GetFullName())
        };

        var claimsIdentity = new ClaimsIdentity(claims, nameof(BuberDinnerAuthHandler));
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));

    }
}