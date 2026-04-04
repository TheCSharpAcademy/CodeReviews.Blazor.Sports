namespace SportsStatistics.Domain.MatchTracking.PlayerEvents;

public sealed class PlayerEvent : MatchEventBase
{
    private PlayerEvent(Guid fixtureId,
                        Guid playerId,
                        PlayerEventType type,
                        Minute minute,
                        DateTime occurredAtUtc)
        : base(fixtureId, minute, occurredAtUtc)
    {
        PlayerId = playerId;
        Type = type;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PlayerEvent"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private PlayerEvent() { }

    public Guid PlayerId { get; private set; } = default!;

    public PlayerEventType Type { get; private set; } = default!;

    public static PlayerEvent Create(Guid fixtureId, Guid playerId, PlayerEventType playerEventType, Minute minute, DateTime occurredAtUtc)
    {
        return new(fixtureId, playerId, playerEventType, minute, occurredAtUtc);
    }
}
