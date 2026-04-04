using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetSeasonStatistics;

public sealed record GetSeasonStatisticsQuery(Guid SeasonId)
    : IQuery<SeasonStatisticsResponse>;
