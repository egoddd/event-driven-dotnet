using MassTransit;
using EventDrivenDotNet.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<EventDrivenDotNet.Worker.Worker>();

builder.Services.AddSingleton<EventDrivenDotNet.Contracts.InMemoryEventBus>();
builder.Services.AddSingleton<EventDrivenDotNet.Contracts.IEventBus>(sp =>
    sp.GetRequiredService<EventDrivenDotNet.Contracts.InMemoryEventBus>());

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PolicyCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();