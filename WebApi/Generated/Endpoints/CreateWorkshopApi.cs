using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.DemoErrorHandling;

namespace WebApi.Endpoints;

public static class CreateWorkshopApi
{
    ///
    /// Vytvoření workshopu
    ///
    public static RouteHandlerBuilder CreateWorkshop(this IEndpointRouteBuilder api)
    {
        return api.MapPost("/workshops", async ([FromBody]CreateWorkshopRequest model, [FromServices]ContractValidator validator, [FromServices]AppDbContext db) =>
        {
            validator.EnsureValid(model);

            return Results.Created();
        });
    }
}
