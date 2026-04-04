using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Pages.Reports.Models;

public sealed record PlayerSeasonStatisticsDto(
    Guid PlayerId,
    string PlayerName,
    Position Position,
    StatPair FixturesPlayed,
    StatPair FixturesStarted,
    StatPair PassCount,
    StatPair PassSuccessCount,
    StatPair PassFailureCount,
    StatPair ShotCount,
    StatPair ShotOnTargetCount,
    StatPair ShotOffTargetCount,
    StatPair GoalCount,
    StatPair GoalAssistCount,
    StatPair OwnGoalCount,
    StatPair SaveCount,
    StatPair TackleCount,
    StatPair FoulWonCount,
    StatPair FoulConcededCount,
    StatPair YellowCardCount,
    StatPair RedCardCount)
{
    public string Title => $"{PlayerName} ({Position.Name})";
}
