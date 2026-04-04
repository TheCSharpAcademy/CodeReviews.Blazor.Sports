using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

public sealed record FixtureResponse(
    Guid Id,
    Guid CompetitionId,
    string CompetitionName,
    string Opponent,
    DateTime KickoffTimeUtc,
    Location Location,
    Score Score,
    Status Status,
    Outcome Outcome);
