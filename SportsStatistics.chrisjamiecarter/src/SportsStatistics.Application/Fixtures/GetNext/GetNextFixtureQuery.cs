using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetNext;

public sealed record GetNextFixtureQuery(
    DateTime TodayEnd)
    : IQuery<FixtureResponse>;
