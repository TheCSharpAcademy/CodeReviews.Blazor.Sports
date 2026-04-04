using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetByDate;

public sealed record GetFixturesByDateQuery(DateOnly FixtureDate) : IQuery<List<FixtureResponse>>;
