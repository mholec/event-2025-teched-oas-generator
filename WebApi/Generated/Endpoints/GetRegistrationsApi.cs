using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;

namespace WebApi.Endpoints;

public static class GetRegistrationsApi
{
    ///
    /// Seznam registrací
    ///
    public static RouteHandlerBuilder GetRegistrations(this IEndpointRouteBuilder api)
    {
        return api.MapGet("/registrations", async ([FromQuery]string workshopId, [FromQuery]int page, [FromQuery]int pageSize, [FromServices]AppDbContext db) =>
        {
            return Results.Ok();
        });
    }
}
