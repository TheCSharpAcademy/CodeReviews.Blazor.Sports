using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetAll;

public sealed record GetAllFixturesQuery() : IQuery<List<FixtureResponse>>;
