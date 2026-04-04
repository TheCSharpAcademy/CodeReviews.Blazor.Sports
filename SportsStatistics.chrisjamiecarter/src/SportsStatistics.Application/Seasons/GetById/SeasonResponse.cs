namespace SportsStatistics.Application.Seasons.GetById;

public sealed record SeasonResponse(Guid Id,
                                    DateOnly StartDate,
                                    DateOnly EndDate,
                                    string Name);
