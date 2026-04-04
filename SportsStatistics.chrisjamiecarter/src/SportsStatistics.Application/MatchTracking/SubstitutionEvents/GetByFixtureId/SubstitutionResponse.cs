namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.GetByFixtureId;

/// <summary>
/// Response model for substitution event data.
/// </summary>
/// <param name="Id">The unique identifier for the substitution event.</param>
/// <param name="PlayerOffId">The player being substituted off.</param>
/// <param name="PlayerOnId">The player being substituted on.</param>
/// <param name="BaseMinute">The base minute (1-90).</param>
/// <param name="StoppageMinute">The stoppage minute offset if applicable.</param>
/// <param name="DisplayMinute">The display notation (e.g., "45", "45+1", "90+2").</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the event occurred.</param>
public sealed record SubstitutionResponse(
    Guid Id,
    Guid PlayerOffId,
    Guid PlayerOnId,
    int BaseMinute,
    int? StoppageMinute,
    string DisplayMinute,
    DateTime OccurredAtUtc);
