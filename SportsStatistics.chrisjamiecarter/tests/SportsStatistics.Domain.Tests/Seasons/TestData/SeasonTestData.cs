using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Domain.Tests.Seasons.TestData;

public static class SeasonTestData
{
    public static readonly DateRange DateRange = DateRangeTestData.ValidDateRange;

    public static Season ValidSeason => Season.Create(DateRange);
}
