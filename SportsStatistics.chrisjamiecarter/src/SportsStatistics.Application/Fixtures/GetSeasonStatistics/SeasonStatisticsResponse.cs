namespace SportsStatistics.Application.Fixtures.GetSeasonStatistics;

public sealed record SeasonStatisticsResponse(
    int GamesPlayed,
    int Wins,
    int Draws,
    int Losses,
    int GoalsFor,
    int GoalsAgainst,
    int GoalDifference);
