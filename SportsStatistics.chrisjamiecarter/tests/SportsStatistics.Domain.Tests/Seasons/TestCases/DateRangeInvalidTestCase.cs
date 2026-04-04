using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class DateRangeInvalidTestCase : TheoryData<DateOnly?, DateOnly?, Error>
{
    public DateRangeInvalidTestCase()
    {
        Add(DateRangeTestData.NullStartDate, DateRangeTestData.ValidEndDate, SeasonErrors.DateRange.StartDate.NullOrEmpty);
        Add(DateRangeTestData.EmptyStartDate, DateRangeTestData.ValidEndDate, SeasonErrors.DateRange.StartDate.NullOrEmpty);
        Add(DateRangeTestData.ValidStartDate, DateRangeTestData.NullEndDate, SeasonErrors.DateRange.EndDate.NullOrEmpty);
        Add(DateRangeTestData.ValidStartDate, DateRangeTestData.EmptyEndDate, SeasonErrors.DateRange.EndDate.NullOrEmpty);
        Add(DateRangeTestData.ValidEndDate, DateRangeTestData.ValidStartDate, SeasonErrors.DateRange.StartDateAfterEndDate);
    }
}
