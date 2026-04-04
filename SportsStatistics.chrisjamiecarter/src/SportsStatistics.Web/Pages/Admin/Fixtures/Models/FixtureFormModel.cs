namespace SportsStatistics.Web.Pages.Admin.Fixtures.Models;

public class FixtureFormModel
{
    public SeasonDto? Season { get; set; }

    public CompetitionDto? Competition { get; set; }

    public DateTime? KickoffDateLocal { get; set; }

    public DateTime? KickoffTimeLocal { get; set; }

    public LocationOptionDto? Location { get; set; }

    public string Opponent { get; set; } = string.Empty;
}
