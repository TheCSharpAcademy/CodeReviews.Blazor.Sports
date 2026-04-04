using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class CreateSeasonTestCase : TheoryData<DateRange>
{
    public CreateSeasonTestCase()
    {
        Add(DateRangeTestData.ValidDateRange);
    }
}
