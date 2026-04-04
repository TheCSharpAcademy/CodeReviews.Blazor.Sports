using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.Create;

public sealed record CreateCompetitionCommand(Guid SeasonId,
                                              string Name,
                                              int FormatId) : ICommand;
