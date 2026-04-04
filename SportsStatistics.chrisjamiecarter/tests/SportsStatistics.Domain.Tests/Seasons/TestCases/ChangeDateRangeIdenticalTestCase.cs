using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class ChangeDateRangeIdenticalTestCase : TheoryData<Season, DateRange>
{
    private static readonly Season Season = SeasonTestData.ValidSeason;

    private static readonly DateRange IdenticalDateRange = DateRange.Create(Season.DateRange.StartDate, Season.DateRange.EndDate).Value;

    public ChangeDateRangeIdenticalTestCase()
    {
        Add(Season, IdenticalDateRange);
    }
}
