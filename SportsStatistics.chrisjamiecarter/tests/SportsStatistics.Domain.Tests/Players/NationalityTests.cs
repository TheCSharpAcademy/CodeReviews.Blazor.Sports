using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class NationalityTests
{
    [Theory]
    [ClassData(typeof(NationalityInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenNationalityIsInvalid(string? nationality, Error expected)
    {
        // Arrange.
        // Act.
        var result = Nationality.Create(nationality);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(NationalityValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenNationalityIsValid(string nationality, Nationality expected)
    {
        // Arrange.
        // Act.
        var result = Nationality.Create(nationality);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
