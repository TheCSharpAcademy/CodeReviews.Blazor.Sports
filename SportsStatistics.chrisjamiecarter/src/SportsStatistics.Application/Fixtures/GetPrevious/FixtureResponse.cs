using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetPrevious;

public sealed record FixtureResponse(
    Guid Id,
    string CompetitionName,
    string Opponent,
    DateTime KickoffTimeUtc,
    Location Location,
    Score Score,
    Outcome Outcome);
