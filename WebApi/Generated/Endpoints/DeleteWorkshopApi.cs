using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;

namespace WebApi.Endpoints;

public static class DeleteWorkshopApi
{
    ///
    /// Odstranění workshopu
    ///
    public static RouteHandlerBuilder DeleteWorkshop(this IEndpointRouteBuilder api)
    {
        return api.MapDelete("/workshops/{id:apid}", async ([FromRoute]string id, [FromServices]AppDbContext db) =>
        {
            return Results.NoContent();
        });
    }
}
