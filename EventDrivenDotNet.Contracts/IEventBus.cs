namespace EventDrivenDotNet.Contracts;

public interface IEventBus
{
    void Subscribe<T>(Func<T, CancellationToken, Task> handler);
    Task PublishAsync<T>(T @event, CancellationToken ct = default);
}