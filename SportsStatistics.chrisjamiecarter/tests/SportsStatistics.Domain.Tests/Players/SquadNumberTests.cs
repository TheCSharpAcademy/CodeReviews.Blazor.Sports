using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class SquadNumberTests
{
    [Theory]
    [ClassData(typeof(SquadNumberInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenSquadNumberIsInvalid(int? squadNumber, Error expected)
    {
        // Arrange.
        // Act.
        var result = SquadNumber.Create(squadNumber);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(SquadNumberValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenSquadNumberIsValid(int squadNumber, SquadNumber expected)
    {
        // Arrange.
        // Act.
        var result = SquadNumber.Create(squadNumber);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
