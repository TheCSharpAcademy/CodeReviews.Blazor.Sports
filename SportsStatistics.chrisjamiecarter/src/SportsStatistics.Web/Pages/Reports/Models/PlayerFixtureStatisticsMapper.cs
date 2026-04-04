using SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

namespace SportsStatistics.Web.Pages.Reports.Models;

internal static class PlayerFixtureStatisticsMapper
{
    public static IQueryable<PlayerFixtureStatisticsDto> ToQueryable(this List<PlayerFixtureStatisticsResponse> statistics) =>
        statistics.Select(ToDto).AsQueryable();

    public static PlayerFixtureStatisticsDto ToDto(this PlayerFixtureStatisticsResponse statistic) => new(
        statistic.PlayerId,
        statistic.PlayerName,
        statistic.Position,
        statistic.MinutesPlayed,
        statistic.PassCount,
        statistic.PassSuccessCount,
        statistic.PassFailureCount,
        statistic.ShotCount,
        statistic.ShotOnTargetCount,
        statistic.ShotOffTargetCount,
        statistic.GoalCount,
        statistic.GoalAssistCount,
        statistic.OwnGoalCount,
        statistic.SaveCount,
        statistic.TackleCount,
        statistic.FoulWonCount,
        statistic.FoulConcededCount,
        statistic.YellowCardCount,
        statistic.RedCardCount);
}
