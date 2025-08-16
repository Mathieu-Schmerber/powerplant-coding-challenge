using System.Text.Json;
using System.Text.Json.Serialization;
using CodingChallenge.Configurations;
using CodingChallenge.Converters;
using CodingChallenge.Factories;
using CodingChallenge.Interfaces;
using CodingChallenge.Middlewares;
using CodingChallenge.Models;
using CodingChallenge.Services;
using CodingChallenge.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.Configure<PowerPlantConfig>(builder.Configuration.GetSection(PowerPlantConfig.SECTION_NAME));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new FloatConverter());
    
    options.SerializerOptions.AllowTrailingCommas = false;
    options.SerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
});

// Services
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPowerPlantFactory, PowerPlantFactory>();
builder.Services.AddScoped<IProductionPlanService, ProductionPlanService>();
builder.Services.AddScoped<IMeritOrderAlgorithm, MeritOrderAlgorithm>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductionPlanRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();
else
    app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapPost("/productionplan", async (
    ProductionPlanRequest request, 
    HttpContext context,
    IValidator<ProductionPlanRequest> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors
            .Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }

    var service = context.RequestServices.GetRequiredService<IProductionPlanService>();
    var productionPlan = await service.GetProductionPlan(request);

    return !productionPlan.Feasible
        ? Results.BadRequest()
        : Results.Ok(productionPlan.PowerPlantLoads);
});

app.Run();