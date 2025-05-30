using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;
using WebApi.Ext;

namespace WebApi.Endpoints;

public static class GetWorkshopApi
{
    ///
    /// Detail workshopu
    ///
    public static RouteHandlerBuilder GetWorkshop(this IEndpointRouteBuilder api)
    {
        return api.MapGet("/workshops/{id:apid}", async ([FromRoute]string id, [FromServices]AppDbContext db) =>
        {
            var result = db.Courses.ToContract().FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });
    }
}