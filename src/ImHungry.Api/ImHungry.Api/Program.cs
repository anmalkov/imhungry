var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/v1", () =>
{
    return "Food trucks";
})
.WithGroupName("v1")  // MinimalAPI does not support versioning now, so this is as a workaround
.WithName("GetTrucks");

app.Run();