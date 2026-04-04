using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetTodays;

public sealed record FixtureResponse(
    Guid Id,
    string CompetitionName,
    string Opponent,
    DateTime KickoffTimeUtc,
    Location Location,
    Status Status,
    Score Score,
    Outcome Outcome);
