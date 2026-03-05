var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<EventDrivenDotNet.Worker.Worker>();

builder.Services.AddSingleton<EventDrivenDotNet.Contracts.InMemoryEventBus>();
builder.Services.AddSingleton<EventDrivenDotNet.Contracts.IEventBus>(sp =>
    sp.GetRequiredService<EventDrivenDotNet.Contracts.InMemoryEventBus>());

var host = builder.Build();
host.Run();