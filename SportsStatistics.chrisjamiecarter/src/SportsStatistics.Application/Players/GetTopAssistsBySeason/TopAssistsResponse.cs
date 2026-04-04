using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Players.GetTopAssistsBySeason;

public sealed record TopAssistsResponse(
    Guid PlayerId,
    string PlayerName,
    Position Position,
    int AssistCount);
