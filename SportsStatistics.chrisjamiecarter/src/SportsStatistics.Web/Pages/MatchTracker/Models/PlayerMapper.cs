using SportsStatistics.Application.Players.GetAll;

namespace SportsStatistics.Web.Pages.MatchTracker.Models;

internal static class PlayerMapper
{
    public static PlayerDto ToDto(this PlayerResponse player)
        => new(player.Id,
               player.Name,
               player.SquadNumber,
               player.Nationality,
               player.DateOfBirth,
               player.Position.Value,
               player.Position.Name,
               player.Age);

    public static IEnumerable<PlayerOptionDto> ToOptions(this IEnumerable<PlayerDto> players) 
        => players
        .OrderBy(player => player.SquadNumber)
        .Select(player => new PlayerOptionDto(player.Id, player.Display));

    //public static PlayerFormModel ToInputModel(this PlayerDto player, IEnumerable<PositionOptionDto> positionOptions)
    //    => new()
    //    {
    //        Name = player.Name,
    //        SquadNumber = player.SquadNumber,
    //        Nationality = player.Nationality,
    //        DateOfBirth = player.DateOfBirth.ToDateTime(),
    //        Position = positionOptions.SingleOrDefault(option => option.Value == player.PostionId),
    //    };

    //public static IQueryable<PlayerDto> ToQueryable(this List<PlayerResponse> players)
    //    => players.Select(ToDto).AsQueryable();
}
