namespace SportsStatistics.Application.Seasons.GetCurrent;

public sealed record SeasonResponse(
    Guid SeasonId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Name);
