using SportsStatistics.Domain.Players;

namespace SportsStatistics.Web.Pages.Admin.Players.Models;

internal static class PositionOptionDtoMapping
{
    public static PositionOptionDto ToDto(this Position position)
        => new(position.Value, position.Name);
}
