using CodingChallenge.Configurations;
using CodingChallenge.Factories;
using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Services;
using CodingChallenge.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.Configure<PowerPlantConfig>(builder.Configuration.GetSection(PowerPlantConfig.SECTION_NAME));

// Services
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPowerPlantFactory, PowerPlantFactory>();
builder.Services.AddScoped<IProductionPlanService, ProductionPlanService>();
builder.Services.AddScoped<IMeritOrderAlgorithm, MeritOrderAlgorithm>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.MapPost("/productionplan", async (ProductionPlanRequest request, HttpContext context) =>
{
    var service = context.RequestServices.GetRequiredService<IProductionPlanService>();
    var productionPlan = await service.GetProductionPlan(request);
    return !productionPlan.Feasible ? Results.BadRequest() : Results.Ok(productionPlan.PowerPlantLoads);
});

app.Run();