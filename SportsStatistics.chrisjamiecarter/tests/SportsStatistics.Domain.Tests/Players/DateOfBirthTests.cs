using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class DateOfBirthTests
{
    [Theory]
    [ClassData(typeof(DateOfBirthInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenDateOfBirthIsInvalid(DateOnly? dateOfBirth, Error expected)
    {
        // Arrange.
        // Act.
        var result = DateOfBirth.Create(dateOfBirth);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(DateOfBirthValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenDateOfBirthIsValid(DateOnly dateOfBirth, DateOfBirth expected)
    {
        // Arrange.
        // Act.
        var result = DateOfBirth.Create(dateOfBirth);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
