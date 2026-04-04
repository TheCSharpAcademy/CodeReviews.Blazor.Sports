using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Domain.Tests.Seasons.TestData;

public class DateRangeTestData
{
    public static DateOnly ValidStartDate => new(DateTime.UtcNow.Year, 1, 1);
    public static DateOnly ValidEndDate => new(DateTime.UtcNow.Year, 12, 31);

    public static readonly DateRange ValidDateRange = DateRange.Create(ValidStartDate, ValidEndDate).Value;

    public static readonly DateOnly? NullStartDate;

    public static readonly DateOnly? NullEndDate;

    public static readonly DateOnly EmptyStartDate = DateOnly.MinValue;

    public static readonly DateOnly EmptyEndDate = DateOnly.MinValue;
}
