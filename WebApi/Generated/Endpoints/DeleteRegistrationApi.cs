using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;

namespace WebApi.Endpoints;

public static class DeleteRegistrationApi
{
    ///
    /// Odstranění registrace
    ///
    public static RouteHandlerBuilder DeleteRegistration(this IEndpointRouteBuilder api)
    {
        return api.MapDelete("/registrations/{id:guid}", async ([FromRoute]Guid id, [FromServices]AppDbContext db) =>
        {
            return Results.NoContent();
        });
    }
}
