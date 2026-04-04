using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ScoreValidTestCase : TheoryData<int, int, Score>
{
    private static readonly Score Score = ScoreTestData.ValidScore;
    private static readonly Score ZeroZero = ScoreTestData.ZeroZero;
    private static readonly Score OneNil = ScoreTestData.OneNil;
    private static readonly Score NilOne = ScoreTestData.NilOne;
    private static readonly Score TwoTwo = ScoreTestData.TwoTwo;
    private static readonly Score FiveThree = ScoreTestData.FiveThree;

    public ScoreValidTestCase()
    {
        Add(Score.HomeGoals, Score.AwayGoals, Score);
        Add(ZeroZero.HomeGoals, ZeroZero.AwayGoals, ZeroZero);
        Add(OneNil.HomeGoals, OneNil.AwayGoals, OneNil);
        Add(NilOne.HomeGoals, NilOne.AwayGoals, NilOne);
        Add(TwoTwo.HomeGoals, TwoTwo.AwayGoals, TwoTwo);
        Add(FiveThree.HomeGoals, FiveThree.AwayGoals, FiveThree);
    }
}
