using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.DemoErrorHandling.Exceptions;

namespace WebApi.DemoErrorHandling.Handlers;

public class ApiValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ApiValidationException ave)
        {
            return false;
        }

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails()
        {
            Title = "Validation failed",
            Status = 400,
            Extensions = new Dictionary<string, object>()
            {
                { "errors", ave.Errors.ToList() }
            }
        }, cancellationToken: cancellationToken);

        return true;
    }
}