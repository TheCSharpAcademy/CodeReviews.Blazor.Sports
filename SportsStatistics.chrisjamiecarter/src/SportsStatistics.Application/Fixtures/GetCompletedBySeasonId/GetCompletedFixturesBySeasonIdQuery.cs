using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

public sealed record GetCompletedFixturesBySeasonIdQuery(
    Guid SeasonId)
    : IQuery<List<FixtureResponse>>;
