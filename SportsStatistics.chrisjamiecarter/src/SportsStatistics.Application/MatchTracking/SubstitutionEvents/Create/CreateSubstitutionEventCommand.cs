using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.Create;

/// <summary>
/// Command to create a new substitution event.
/// </summary>
/// <param name="FixtureId">The fixture identifier.</param>
/// <param name="PlayerOffId">The player being substituted off.</param>
/// <param name="PlayerOnId">The player being substituted on.</param>
/// <param name="BaseMinute">The base minute (1-90 for regulation, or 45/90 for stoppage base).</param>
/// <param name="StoppageMinute">The stoppage minute offset (1, 2, 3...) if in stoppage time, otherwise null.</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the event occurred.</param>
public sealed record CreateSubstitutionEventCommand(Guid FixtureId,
                                                    Guid PlayerOffId,
                                                    Guid PlayerOnId,
                                                    int BaseMinute,
                                                    int? StoppageMinute,
                                                    DateTime OccurredAtUtc) : ICommand;
