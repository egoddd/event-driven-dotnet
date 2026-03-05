namespace EventDrivenDotNet.Contracts;

public sealed class InMemoryEventBus : IEventBus
{
    private readonly List<Func<object, CancellationToken, Task>> _handlers = new();

    public void Subscribe<T>(Func<T, CancellationToken, Task> handler)
        => _handlers.Add(async (evt, ct) =>
        {
            if (evt is T typed) await handler(typed, ct);
        });

    public async Task PublishAsync<T>(T @event, CancellationToken ct = default)
    {
        foreach (var handler in _handlers)
            await handler(@event!, ct);
    }
}