using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Players.GetTopScorersBySeason;

public sealed record TopScorersResponse(
    Guid PlayerId,
    string PlayerName,
    Position Position,
    int GoalCount);
