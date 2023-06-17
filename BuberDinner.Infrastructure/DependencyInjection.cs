using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Providers.AuthHandler;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        AddAuthProvider(services);
        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
        
        return services;
    }

    private static IServiceCollection AddAuthProvider(this IServiceCollection services)
    {
        services.AddAuthentication(options => options.DefaultScheme = "BuberDinner")
            .AddScheme<BuberDinnerAuthSchemeOptions, BuberDinnerAuthHandler>("BuberDinner", options => {});

        return services;
    }
}