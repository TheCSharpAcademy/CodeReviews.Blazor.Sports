using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class SeasonCreatedDomainEventTestCase : TheoryData<DateRange>
{
    public SeasonCreatedDomainEventTestCase()
    {
        Add(DateRangeTestData.ValidDateRange);
    }
}
