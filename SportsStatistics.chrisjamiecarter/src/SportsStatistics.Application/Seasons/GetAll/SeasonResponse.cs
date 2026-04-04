namespace SportsStatistics.Application.Seasons.GetAll;

public sealed record SeasonResponse(Guid Id,
                                    DateOnly StartDate,
                                    DateOnly EndDate,
                                    string Name);
