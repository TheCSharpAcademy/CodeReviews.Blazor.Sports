using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

public sealed record GetPlayerSeasonStatisticsQuery(
    Guid SeasonId)
    : IQuery<List<PlayerSeasonStatisticsResponse>>;
