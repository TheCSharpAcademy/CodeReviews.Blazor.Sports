using SportsStatistics.Application.Players.Create;
using SportsStatistics.Application.Players.Update;

namespace SportsStatistics.Web.Pages.Admin.Players.Models;

internal static class PlayerFormModelMapping
{
    public static CreatePlayerCommand ToCommand(this PlayerFormModel player)
    {
        return new(player.Name,
                   player.SquadNumber,
                   player.Nationality,
                   DateOnly.FromDateTime(player.DateOfBirth.GetValueOrDefault()),
                   player.Position?.Value ?? -1);
    }

    public static UpdatePlayerCommand ToCommand(this PlayerFormModel player, Guid playerId)
    {
        return new(playerId,
                   player.Name,
                   player.SquadNumber,
                   player.Nationality,
                   DateOnly.FromDateTime(player.DateOfBirth.GetValueOrDefault()),
                   player.Position?.Value ?? -1,
                   player.LeftClub);
    }
}