using System.Net;
using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/errors")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int) serviceException.StatusCode, serviceException.ErrorMessage),
            _ => ((int) HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };
        
        return Problem(statusCode: statusCode, title: message);
    }
}