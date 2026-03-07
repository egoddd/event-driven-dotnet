using MassTransit;
using EventDrivenDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<EventDrivenDotNet.Contracts.InMemoryEventBus>();
builder.Services.AddSingleton<EventDrivenDotNet.Contracts.IEventBus>(sp => sp.GetRequiredService<EventDrivenDotNet.Contracts.InMemoryEventBus>());

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("/events/policy-created", async (IPublishEndpoint publisher) =>
{
    var evt = new PolicyCreated(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
    await publisher.Publish(evt);
    return Results.Ok(evt);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
