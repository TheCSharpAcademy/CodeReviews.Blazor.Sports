using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Players.GetAll;

internal static class PlayerMapper
{
    public static List<PlayerResponse> ToResponse(this IEnumerable<Player> players)
        => [.. players.Select(ToResponse)];

    public static PlayerResponse ToResponse(this Player player)
        => new(player.Id,
               player.Name,
               player.SquadNumber,
               player.Nationality,
               player.DateOfBirth,
               player.Position,
               player.LeftClub,
               player.LeftClubOnUtc,
               player.Age);
}
