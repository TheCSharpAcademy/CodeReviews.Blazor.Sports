using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.Update;

public sealed record UpdateSeasonCommand(Guid SeasonId,
                                         DateOnly StartDate,
                                         DateOnly EndDate) : ICommand;
