using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Constraints;
using WebApi.Data;
using WebApi.DemoErrorHandling;
using WebApi.DemoErrorHandling.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(x => x.ConstraintMap.Add("apid", typeof(ApidRouteConstraint)));

builder.Services.AddDbContext<AppDbContext>(x=> x.UseInMemoryDatabase("demo"));
builder.Services.AddHostedService<DatabaseInitializer>();

builder.Services.AddScoped<ContractValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddExceptionHandler<ApiEmptyBodyExceptionHandler>();
builder.Services.AddExceptionHandler<ApiValidationExceptionHandler>();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.MapGroup("").RegisterApiRoutes();

app.Run();