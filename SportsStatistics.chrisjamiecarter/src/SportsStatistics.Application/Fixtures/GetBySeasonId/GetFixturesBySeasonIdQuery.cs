using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

public sealed record GetFixturesBySeasonIdQuery(Guid SeasonId) : IQuery<List<FixtureResponse>>;
