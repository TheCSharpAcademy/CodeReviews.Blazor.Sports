using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Update;

public sealed record UpdateFixtureCommand(Guid FixtureId,
                                          string Opponent,
                                          DateTime KickoffTimeUtc,
                                          int LocationId) : ICommand;
