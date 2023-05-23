using BuberDinner.Api.Common.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplicationLayer();
    builder.Services.AddInfrastructureLayer();
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}


var app = builder.Build();
{
    app.UseHttpsRedirection();

    app.UseExceptionHandler("/errors");
    
    app.MapControllers();
    app.MapGet("/", () => "Hello World!");

    app.Run();
}
