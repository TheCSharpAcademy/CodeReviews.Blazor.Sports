using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.Update;

public sealed record UpdatePlayerCommand(Guid PlayerId,
                                         string Name,
                                         int SquadNumber,
                                         string Nationality,
                                         DateOnly DateOfBirth,
                                         int PositionId,
                                         bool LeftClub) : ICommand;
