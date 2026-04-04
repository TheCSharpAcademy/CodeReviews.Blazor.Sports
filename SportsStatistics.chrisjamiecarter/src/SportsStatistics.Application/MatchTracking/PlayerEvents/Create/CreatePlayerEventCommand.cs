using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.PlayerEvents.Create;

/// <summary>
/// Command to create a new player event.
/// </summary>
/// <param name="FixtureId">The fixture identifier.</param>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="PlayerEventTypeId">The player event type identifier.</param>
/// <param name="BaseMinute">The base minute (1-90 for regulation, or 45/90 for stoppage base).</param>
/// <param name="StoppageMinute">The stoppage minute offset (1, 2, 3...) if in stoppage time, otherwise null.</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the event occurred.</param>
public sealed record CreatePlayerEventCommand(Guid FixtureId,
                                              Guid PlayerId,
                                              int PlayerEventTypeId,
                                              int BaseMinute,
                                              int? StoppageMinute,
                                              DateTime OccurredAtUtc) : ICommand;
