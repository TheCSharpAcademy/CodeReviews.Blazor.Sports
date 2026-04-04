using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Players.GetAll;

public sealed record PlayerResponse(
    Guid Id,
    string Name,
    int SquadNumber,
    string Nationality,
    DateOnly DateOfBirth,
    Position Position,
    bool LeftClub,
    DateTime? LeftClubOnUtc,
    int Age);
