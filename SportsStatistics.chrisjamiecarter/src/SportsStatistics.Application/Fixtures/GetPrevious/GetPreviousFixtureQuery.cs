using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetPrevious;

public sealed record GetPreviousFixtureQuery(
    DateTime TodayStart)
    : IQuery<FixtureResponse>;
