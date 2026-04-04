using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.MatchEvents.Create;

/// <summary>
/// Command to create a new match event.
/// </summary>
/// <param name="FixtureId">The fixture identifier.</param>
/// <param name="MatchEventTypeId">The match event type identifier.</param>
/// <param name="BaseMinute">The base minute (1-120 for regulation/extra time, or 45/90/105/120 for stoppage base).</param>
/// <param name="StoppageMinute">The stoppage minute offset (1, 2, 3...) if in stoppage time, otherwise null.</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the event occurred.</param>
public sealed record CreateMatchEventCommand(Guid FixtureId,
                                             int MatchEventTypeId,
                                             int BaseMinute,
                                             int? StoppageMinute,
                                             DateTime OccurredAtUtc) : ICommand;
