using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

public sealed record PlayerFixtureStatistics(
    Guid PlayerId,
    string PlayerName,
    Position Position,
    int MinutesPlayed,
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
