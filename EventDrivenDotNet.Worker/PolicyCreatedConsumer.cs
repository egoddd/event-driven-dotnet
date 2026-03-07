using MassTransit;
using EventDrivenDotNet.Contracts;

namespace EventDrivenDotNet.Worker;

public sealed class PolicyCreatedConsumer : IConsumer<PolicyCreated>
{
    private readonly ILogger<PolicyCreatedConsumer> _logger;

    public PolicyCreatedConsumer(ILogger<PolicyCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<PolicyCreated> context)
    {
        var evt = context.Message;

        _logger.LogInformation(
            "Handled PolicyCreated event for User {UserId} Policy {PolicyId}",
            evt.UserId,
            evt.PolicyId
        );

        return Task.CompletedTask;
    }
}