using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.GetTopScorersBySeason;

public sealed record GetTopScorersBySeasonQuery(
    Guid SeasonId,
    int Count = 3)
    : IQuery<List<TopScorersResponse>>;
