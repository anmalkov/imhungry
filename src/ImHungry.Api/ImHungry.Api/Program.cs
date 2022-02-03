using ImHungry.Api.Helpers;
using ImHungry.Api.Models;
using ImHungry.Api.Repositories;
using ImHungry.Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IMobileFoodPointsRepository, MobileFoodPointsRepository>();
builder.Services.AddSingleton<IMobileFoodPointsService, MobileFoodPointsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/v1/{location}", async (LatLng? location, int? count, HttpRequest request, IMobileFoodPointsService mobileFoodPointsService) =>
{
    if (location is null)
    {
        return Results.BadRequest(new ErrorDto($"location is not specified. It should be in '<latitude>,<longitude>' format. Example: {request.Scheme}://{request.Host}/v1/37.77638040110528,-122.42590558289392"));
    }

    if (count is null)
    {
        count = 5;
    }

    var nearestPointsDto = Mapper.MapToDto(await mobileFoodPointsService.GetNearestAsync(location, count.Value));
    return Results.Ok(nearestPointsDto);
})
.WithGroupName("v1")  // MinimalAPI does not support versioning now, so this is as a workaround
.WithName("GetTrucks");

app.Run();