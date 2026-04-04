using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Fixtures;

public class ScoreTests
{
    [Theory]
    [ClassData(typeof(ScoreInvalidTestCase))]
    public void Create_ShouldReturnFailure_WhenGoalsAreInvalid(int homeGoals, int awayGoals, Error expectedError)
    {
        // Arrange.
        // Act.
        var result = Score.Create(homeGoals, awayGoals);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expectedError);
    }

    [Theory]
    [ClassData(typeof(ScoreValidTestCase))]
    public void Create_ShouldReturnScore_WhenGoalsAreValid(int homeGoals, int awayGoals, Score expectedScore)
    {
        // Arrange.
        // Act.
        var result = Score.Create(homeGoals, awayGoals);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expectedScore);
        result.Value.HomeGoals.ShouldBe(homeGoals);
        result.Value.AwayGoals.ShouldBe(awayGoals);
    }
}
