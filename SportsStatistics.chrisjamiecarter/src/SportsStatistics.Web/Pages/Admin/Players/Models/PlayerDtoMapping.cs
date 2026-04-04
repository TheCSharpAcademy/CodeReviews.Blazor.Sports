using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using SportsStatistics.Application.Players.GetAll;

namespace SportsStatistics.Web.Pages.Admin.Players.Models;

internal static class PlayerDtoMapping
{
    public static PlayerDto ToDto(this PlayerResponse player)
        => new(player.Id,
               player.Name,
               player.SquadNumber,
               player.Nationality,
               player.DateOfBirth,
               player.Position,
               player.LeftClub,
               player.LeftClubOnUtc,
               player.Age);

    public static PlayerFormModel ToInputModel(this PlayerDto player, IEnumerable<PositionOptionDto> positionOptions)
        => new()
        {
            Name = player.Name,
            SquadNumber = player.SquadNumber,
            Nationality = player.Nationality,
            DateOfBirth = player.DateOfBirth.ToDateTime(),
            Position = positionOptions.SingleOrDefault(option => option.Value == player.Position.Value),
            LeftClub = player.LeftClub,
        };

    public static IQueryable<PlayerDto> ToQueryable(this List<PlayerResponse> players)
        => players.Select(ToDto).AsQueryable();
}
