using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

public sealed record PlayerSeasonStatistics(
    Guid PlayerId,
    string PlayerName,
    Position Position,
    int FixturesPlayed,
    int FixturesStarted,
    int PassSuccessCount,
    int PassFailureCount,
    int ShotOnTargetCount,
    int ShotOffTargetCount,
    int GoalCount,
    int GoalAssistCount,
    int OwnGoalCount,
    int SaveCount,
    int TackleCount,
    int FoulWonCount,
    int FoulConcededCount,
    int YellowCardCount,
    int RedCardCount)
{
    public int PassCount => PassSuccessCount + PassFailureCount;
    public int ShotCount => ShotOnTargetCount + ShotOffTargetCount;
}
