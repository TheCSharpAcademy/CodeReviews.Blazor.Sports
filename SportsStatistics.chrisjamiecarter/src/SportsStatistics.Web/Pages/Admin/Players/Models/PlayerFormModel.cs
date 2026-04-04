namespace SportsStatistics.Web.Pages.Admin.Players.Models;

public sealed class PlayerFormModel
{
    public string Name { get; set; } = string.Empty;

    public int SquadNumber { get; set; }

    public string Nationality { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public PositionOptionDto? Position { get; set; }

    public bool LeftClub { get; set; }
}
