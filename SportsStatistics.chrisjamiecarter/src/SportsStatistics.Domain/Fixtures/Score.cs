using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record Score
{
    private Score(int homeGoals, int awayGoals)
    {
        HomeGoals = homeGoals;
        AwayGoals = awayGoals;
    }

    public int HomeGoals { get; private set; }

    public int AwayGoals { get; private set; }

    public static Result<Score> Create(int homeGoals, int awayGoals)
    {
        if (homeGoals < 0)
        {
            return FixtureErrors.Score.HomeGoalsMustBeNonNegative;
        }

        if (awayGoals < 0)
        {
            return FixtureErrors.Score.AwayGoalsMustBeNonNegative;
        }

        return new Score(homeGoals, awayGoals);
    }
}
