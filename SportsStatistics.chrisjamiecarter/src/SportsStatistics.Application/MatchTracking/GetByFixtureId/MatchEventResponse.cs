using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

/// <summary>
/// Response model for match event data.
/// </summary>
/// <param name="Id">The unique identifier for the match event.</param>
/// <param name="FixtureId">The fixture identifier.</param>
/// <param name="EventTypeDisplay">The name of the event type to display.</param>
/// <param name="MinuteDisplay">The notation (e.g., "45", "45+1", "90+2") to display.</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the event occurred.</param>
/// <param name="PlayerId">The player identifier if this is a player event.</param>
public sealed record MatchEventResponse(Guid Id,
                                        Guid FixtureId,
                                        string EventTypeName,
                                        string EventTypeDisplay,
                                        string MinuteDisplay,
                                        DateTime OccurredAtUtc,
                                        Guid? PlayerId);
