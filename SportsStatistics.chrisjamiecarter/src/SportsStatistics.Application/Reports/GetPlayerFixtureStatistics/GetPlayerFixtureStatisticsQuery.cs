using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

public sealed record GetPlayerFixtureStatisticsQuery(
    Guid FixtureId)
    : IQuery<List<PlayerFixtureStatisticsResponse>>;
