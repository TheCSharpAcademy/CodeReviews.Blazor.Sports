using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class DeleteSeasonTestCase : TheoryData<Season, DateTime>
{
    private static readonly Season Season = SeasonTestData.ValidSeason;

    public DeleteSeasonTestCase()
    {
        Add(Season, DateTime.UtcNow);
    }
}
