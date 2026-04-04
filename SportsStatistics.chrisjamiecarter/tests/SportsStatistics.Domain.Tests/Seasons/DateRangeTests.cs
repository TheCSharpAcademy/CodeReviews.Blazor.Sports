using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Seasons;

public class DateRangeTests
{
    [Theory]
    [ClassData(typeof(DateRangeInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenDateRangeIsInvalid(DateOnly? startDate, DateOnly? endDate, Error expected)
    {
        // Arrange.
        // Act.
        var result = DateRange.Create(startDate, endDate);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(DateRangeValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenDateRangeIsValid(DateOnly startDate, DateOnly endDate, DateRange expected)
    {
        // Arrange.
        // Act.
        var result = DateRange.Create(startDate, endDate);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
