namespace EventDrivenDotNet.Contracts;

public sealed record PolicyCreated(
    Guid PolicyId,
    Guid UserId,
    DateTime OccurredAtUtc
);