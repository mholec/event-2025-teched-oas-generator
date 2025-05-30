using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.DemoErrorHandling.Exceptions;

namespace WebApi.DemoErrorHandling.Handlers;

public class ApiEmptyBodyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ApiEmptyBodyException ave)
        {
            return false;
        }

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Title = "Validation failed",
            Status = 400,
            Detail = "Empty body is not allowed",
        }, cancellationToken: cancellationToken);

        return true;
    }
}