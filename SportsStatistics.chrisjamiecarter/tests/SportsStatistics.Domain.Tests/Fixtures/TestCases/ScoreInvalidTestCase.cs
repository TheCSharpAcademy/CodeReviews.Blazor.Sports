using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ScoreInvalidTestCase : TheoryData<int, int, Error>
{
    private static readonly Score Score = ScoreTestData.ValidScore;

    public ScoreInvalidTestCase()
    {
        Add(ScoreTestData.NegativeHomeGoals, Score.AwayGoals, FixtureErrors.Score.HomeGoalsMustBeNonNegative);
        Add(Score.HomeGoals, ScoreTestData.NegativeAwayGoals, FixtureErrors.Score.AwayGoalsMustBeNonNegative);
        Add(ScoreTestData.NegativeHomeGoals, ScoreTestData.NegativeAwayGoals, FixtureErrors.Score.HomeGoalsMustBeNonNegative);
    }
}
