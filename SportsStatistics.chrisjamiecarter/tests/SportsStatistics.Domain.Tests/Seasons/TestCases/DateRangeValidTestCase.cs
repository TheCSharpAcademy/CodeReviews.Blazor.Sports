using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class DateRangeValidTestCase : TheoryData<DateOnly, DateOnly, DateRange>
{
    public DateRangeValidTestCase()
    {
        Add(DateRangeTestData.ValidDateRange.StartDate, DateRangeTestData.ValidDateRange.EndDate, DateRangeTestData.ValidDateRange);
    }
}
