using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Domain.Tests.Fixtures.TestData;

public static class ScoreTestData
{
    public static Score ValidScore => Score.Create(3, 2).Value;

    public static Score ZeroZero => Score.Create(0, 0).Value;

    public static Score OneNil => Score.Create(1, 0).Value;

    public static Score NilOne => Score.Create(0, 1).Value;

    public static Score TwoTwo => Score.Create(2, 2).Value;

    public static Score FiveThree => Score.Create(5, 3).Value;

    public static readonly int NegativeHomeGoals = -1;

    public static readonly int NegativeAwayGoals = -1;
}
