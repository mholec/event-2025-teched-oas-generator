using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.DemoErrorHandling.Contracts;
using WebApi.DemoErrorHandling.Exceptions;

namespace WebApi.DemoErrorHandling.Handlers;

public class ApiExceptionHandler(IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        string title = exception is ApiException ? exception.Message : "Unexpected error occurred";
        string detail = env.IsDevelopment() ? exception.StackTrace : null;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Title = title,
            Status = 500,
            Detail = detail,
        }, cancellationToken: cancellationToken);

        return true;
    }
}