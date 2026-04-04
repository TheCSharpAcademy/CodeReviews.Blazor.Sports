namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public sealed class MatchEvent : MatchEventBase
{
    private MatchEvent(Guid fixtureId,
                       MatchEventType type,
                       Minute minute,
                       DateTime occurredAtUtc)
        : base(fixtureId, minute, occurredAtUtc)
    {
        Type = type;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="MatchEvent"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private MatchEvent() { }

    public MatchEventType Type { get; private set; } = default!;

    public static MatchEvent Create(Guid fixtureId, MatchEventType matchEventType, Minute minute, DateTime occurredAtUtc)
    {
        return new(fixtureId, matchEventType, minute, occurredAtUtc);
    }
}
