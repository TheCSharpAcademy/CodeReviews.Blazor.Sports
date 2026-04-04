namespace SportsStatistics.Web.Pages.Admin.Seasons.Models;

public class SeasonFormModel
{
    public SeasonFormModel()
    {
        var defaultStart = new DateTime(DateTime.UtcNow.Year, 7, 1);
        StartDate = defaultStart;

        var defaultEnd = defaultStart.AddYears(1).AddDays(-1);
        EndDate = defaultEnd;
    }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
