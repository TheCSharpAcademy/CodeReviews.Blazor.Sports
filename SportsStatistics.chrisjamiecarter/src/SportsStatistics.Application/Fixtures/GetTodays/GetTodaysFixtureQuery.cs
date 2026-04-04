using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetTodays;

public sealed record GetTodaysFixtureQuery
    : IQuery<FixtureResponse>;
