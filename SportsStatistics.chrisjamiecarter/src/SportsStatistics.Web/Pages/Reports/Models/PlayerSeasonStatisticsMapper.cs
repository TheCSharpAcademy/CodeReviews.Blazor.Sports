using SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

namespace SportsStatistics.Web.Pages.Reports.Models;

internal static class PlayerSeasonStatisticsMapper
{
    public static IQueryable<PlayerSeasonStatisticsDto> ToQueryable(this List<PlayerSeasonStatisticsResponse> statistics) =>
        statistics.Select(ToDto).AsQueryable();

    public static PlayerSeasonStatisticsDto ToDto(this PlayerSeasonStatisticsResponse statistic) => new(
        statistic.PlayerId,
        statistic.PlayerName,
        statistic.Position,
        new StatPair(statistic.FixturesPlayed, statistic.MaxFixturesPlayed),
        new StatPair(statistic.FixturesStarted, statistic.MaxFixturesStarted),
        new StatPair(statistic.PassCount, statistic.MaxPassCount),
        new StatPair(statistic.PassSuccessCount, statistic.MaxPassSuccessCount),
        new StatPair(statistic.PassFailureCount, statistic.MaxPassFailureCount),
        new StatPair(statistic.ShotCount, statistic.MaxShotCount),
        new StatPair(statistic.ShotOnTargetCount, statistic.MaxShotOnTargetCount),
        new StatPair(statistic.ShotOffTargetCount, statistic.MaxShotOffTargetCount),
        new StatPair(statistic.GoalCount, statistic.MaxGoalCount),
        new StatPair(statistic.GoalAssistCount, statistic.MaxGoalAssistCount),
        new StatPair(statistic.OwnGoalCount, statistic.MaxOwnGoalCount),
        new StatPair(statistic.SaveCount, statistic.MaxSaveCount),
        new StatPair(statistic.TackleCount, statistic.MaxTackleCount),
        new StatPair(statistic.FoulWonCount, statistic.MaxFoulWonCount),
        new StatPair(statistic.FoulConcededCount, statistic.MaxFoulConcededCount),
        new StatPair(statistic.YellowCardCount, statistic.MaxYellowCardCount),
        new StatPair(statistic.RedCardCount, statistic.MaxRedCardCount));
}
