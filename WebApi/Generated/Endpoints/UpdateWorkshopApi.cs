using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;

namespace WebApi.Endpoints;

public static class UpdateWorkshopApi
{
    ///
    /// Aktualizace workshopu
    ///
    public static RouteHandlerBuilder UpdateWorkshop(this IEndpointRouteBuilder api)
    {
        return api.MapPut("/workshops/{id:apid}", async ([FromRoute]string id, [FromBody]UpdateWorkshopRequest model, [FromServices]ContractValidator validator, [FromServices]AppDbContext db) =>
        {
            validator.EnsureValid(model);

            return Results.Ok();
        });
    }
}
