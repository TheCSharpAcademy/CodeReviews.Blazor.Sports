using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Create;

public sealed record CreateFixtureCommand(Guid CompetitionId,
                                          string Opponent,
                                          DateTime KickoffTimeUtc,                                          
                                          int LocationId) : ICommand;
