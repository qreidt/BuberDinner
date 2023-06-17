using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentationLayer();
    builder.Services.AddApplicationLayer();
    builder.Services.AddInfrastructureLayer();
}


var app = builder.Build();
{
    app.UseHttpsRedirection();

    app.UseExceptionHandler("/errors");

    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();
    app.MapGet("/", () => "Hello World!");

    app.Run();
}
