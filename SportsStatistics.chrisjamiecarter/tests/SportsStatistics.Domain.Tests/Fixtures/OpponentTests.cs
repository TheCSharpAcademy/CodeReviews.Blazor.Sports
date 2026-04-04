using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Fixtures;

public class OpponentTests
{
    [Theory]
    [ClassData(typeof(OpponentInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenOpponentIsInvalid(string? opponent, Error expected)
    {
        // Arrange.
        // Act.
        var result = Opponent.Create(opponent);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(OpponentValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenOpponentIsValid(string opponent, Opponent expected)
    {
        // Arrange.
        // Act.
        var result = Opponent.Create(opponent);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
