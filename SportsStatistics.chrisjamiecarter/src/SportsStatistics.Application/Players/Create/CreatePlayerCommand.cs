using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Create;

public sealed record CreatePlayerCommand(string Name,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth,
                                         int PositionId) : ICommand;
