using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.GetTopAssistsBySeason;

public sealed record GetTopAssistsBySeasonQuery(
    Guid SeasonId,
    int Count = 3)
    : IQuery<List<TopAssistsResponse>>;
