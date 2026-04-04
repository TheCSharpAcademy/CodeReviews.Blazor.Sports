using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Teamsheets.Create;

public sealed record CreateTeamsheetCommand(Guid FixtureId,
                                            List<Guid> StarterIds) : ICommand;
