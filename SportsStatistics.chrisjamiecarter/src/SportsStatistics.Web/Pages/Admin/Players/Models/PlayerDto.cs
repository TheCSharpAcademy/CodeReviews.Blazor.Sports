using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Pages.Admin.Players.Models;

public sealed record PlayerDto(
    Guid Id,
    string Name,
    int SquadNumber,
    string Nationality,
    DateOnly DateOfBirth,
    Position Position,
    bool LeftClub,
    DateTime? LeftClubOnUtc,
    int Age);
