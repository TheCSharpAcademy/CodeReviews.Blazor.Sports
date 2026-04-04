using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.Create;

public sealed record CreateSeasonCommand(DateOnly StartDate,
                                         DateOnly EndDate) : ICommand;
