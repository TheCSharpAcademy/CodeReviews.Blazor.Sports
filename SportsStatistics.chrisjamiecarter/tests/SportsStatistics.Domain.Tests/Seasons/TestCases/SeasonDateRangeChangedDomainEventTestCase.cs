using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class SeasonDateRangeChangedDomainEventTestCase : TheoryData<Season, DateRange, SeasonDateRangeChangedDomainEvent>
{
    private static readonly Season Season = SeasonTestData.ValidSeason;

    private static readonly DateRange DifferentDateRange = DateRange.Create(Season.DateRange.StartDate.AddDays(1), Season.DateRange.EndDate.AddDays(-1)).Value;

    public SeasonDateRangeChangedDomainEventTestCase()
    {
        Add(Season, DifferentDateRange, new(Season, Season.DateRange));
    }
}
