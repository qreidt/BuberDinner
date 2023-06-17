
using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddControllers();
        
        services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
        
        return services;
    }
}