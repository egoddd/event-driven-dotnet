using System.Text;
using System.Text.Json;
using EventDrivenDotNet.Contracts;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/events/policy-created", async () =>
{
    var factory = new ConnectionFactory
    {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
    };

    await using var connection = await factory.CreateConnectionAsync();
    await using var channel = await connection.CreateChannelAsync();

    await channel.QueueDeclareAsync(
        queue: "policy-created",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    var evt = new PolicyCreated(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evt));

    await channel.BasicPublishAsync(
        exchange: "",
        routingKey: "policy-created",
        mandatory: false,
        body: body);

    return Results.Ok(evt);
});

app.Run();