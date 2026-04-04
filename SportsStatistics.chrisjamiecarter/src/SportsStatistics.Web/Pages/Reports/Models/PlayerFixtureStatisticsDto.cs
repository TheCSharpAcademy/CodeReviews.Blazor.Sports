using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Pages.Reports.Models;

public sealed record PlayerFixtureStatisticsDto(
    Guid PlayerId,
    string PlayerName,
    Position Position,
    int MinutesPlayed,
    int PassCount,
    int PassSuccessCount,
    int PassFailureCount,
    int ShotCount,
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
    int RedCardCount);
