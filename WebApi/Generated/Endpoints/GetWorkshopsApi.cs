using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;

namespace WebApi.Endpoints;

public static class GetWorkshopsApi
{
    ///
    /// Seznam workshopů
    ///
    public static RouteHandlerBuilder GetWorkshops(this IEndpointRouteBuilder api)
    {
        return api.MapGet("/workshops", async ([FromQuery]string searchQuery, [FromQuery]int page, [FromQuery]int pageSize, [FromQuery]string sort, [FromServices]AppDbContext db) =>
        {
            return Results.Ok();
        });
    }
}
