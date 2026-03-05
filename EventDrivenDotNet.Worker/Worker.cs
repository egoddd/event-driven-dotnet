using EventDrivenDotNet.Contracts;

namespace EventDrivenDotNet.Worker;

public sealed class Worker(ILogger<Worker> logger, IEventBus bus) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        bus.Subscribe<PolicyCreated>((evt, ct) =>
        {
            logger.LogInformation("Handled PolicyCreated: {PolicyId} for {UserId} at {Time}",
                evt.PolicyId, evt.UserId, evt.OccurredAtUtc);
            return Task.CompletedTask;
        });

        logger.LogInformation("Worker started and subscribed to events.");
        return Task.Delay(Timeout.Infinite, stoppingToken);
    }
}