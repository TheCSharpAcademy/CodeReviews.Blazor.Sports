using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.Delete;

public sealed record DeleteSeasonCommand(Guid SeasonId) : ICommand;
