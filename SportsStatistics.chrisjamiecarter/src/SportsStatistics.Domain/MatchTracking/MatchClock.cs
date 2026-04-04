namespace SportsStatistics.Domain.MatchTracking;

/// <summary>
/// Represents the live match clock state for tracking elapsed time and current period.
/// </summary>
/// <remarks>
/// <para>
/// The match clock counts upward continuously from 0:00 and never stops or resets within a half.
/// </para>
/// </remarks>
public sealed record MatchClock
{
    /// <summary>
    /// The duration of a regulation half in seconds (45 minutes).
    /// </summary>
    public const int RegulationHalfDurationSeconds = 45 * 60;

    private MatchClock(MatchPeriod period, int elapsedSeconds)
    {
        Period = period;
        ElapsedSeconds = elapsedSeconds;
    }

    /// <summary>
    /// Gets the current match period.
    /// </summary>
    public MatchPeriod Period { get; }

    /// <summary>
    /// Gets the elapsed seconds in the current period.
    /// </summary>
    public int ElapsedSeconds { get; }

    /// <summary>
    /// Gets the current minute based on elapsed time and period.
    /// </summary>
    //public Minute GetCurrentMinute()
    //{
    //    var result = Minute.FromElapsedSeconds(ElapsedSeconds, Period);
    //    return result.IsSuccess ? result.Value : throw new InvalidOperationException($"Cannot determine current minute: {result.Error.Description}");
    //}

    /// <summary>
    /// Determines whether the clock is currently in stoppage time.
    /// </summary>
    //public bool IsInStoppageTime() => Period.IsStoppageTime();

    /// <summary>
    /// Gets the elapsed seconds within stoppage time (if currently in stoppage).
    /// </summary>
    //public int GetStoppageElapsedSeconds()
    //{
    //    if (!IsInStoppageTime())
    //    {
    //        return 0;
    //    }

    //    var periodBoundary = Period.GetStoppageBaseMinute()!.Value * 60;
    //    return ElapsedSeconds - periodBoundary;
    //}

    /// <summary>
    /// Starts the first half of the match.
    /// </summary>
    public static MatchClock StartFirstHalf() => new(MatchPeriod.FirstHalf, 0);

    /// <summary>
    /// Advances the clock by the specified number of seconds.
    /// </summary>
    /// <param name="additionalSeconds">The seconds to add to the clock.</param>
    /// <returns>A new MatchClock with updated elapsed time.</returns>
    public MatchClock Tick(int additionalSeconds)
    {
        if (additionalSeconds < 0)
        {
            throw new ArgumentException("Additional seconds cannot be negative.", nameof(additionalSeconds));
        }

        var newElapsedSeconds = ElapsedSeconds + additionalSeconds;
        return new MatchClock(Period, newElapsedSeconds);
    }

    /// <summary>
    /// Transitions to stoppage time at the end of the current half.
    /// </summary>
    /// <returns>A new MatchClock in stoppage time period.</returns>
    /// <exception cref="InvalidOperationException">Thrown when not at a half boundary.</exception>
    //public MatchClock EnterStoppageTime()
    //{
    //    MatchPeriod stoppagePeriod;

    //    if (Period == MatchPeriod.FirstHalf)
    //    {
    //        stoppagePeriod = MatchPeriod.FirstHalfStoppage;
    //    }
    //    else if (Period == MatchPeriod.SecondHalf)
    //    {
    //        stoppagePeriod = MatchPeriod.SecondHalfStoppage;
    //    }
    //    else
    //    {
    //        throw new InvalidOperationException($"Cannot enter stoppage time from period '{Period.Name}'.");
    //    }

    //    return new MatchClock(stoppagePeriod, ElapsedSeconds);
    //}

    /// <summary>
    /// Starts the second half of the match.
    /// </summary>
    public static MatchClock StartSecondHalf() => new(MatchPeriod.SecondHalf, 0);

    /// <summary>
    /// Ends the first half.
    /// </summary>
    public MatchClock EndFirstHalf() => new(MatchPeriod.HalfTime, ElapsedSeconds);

    /// <summary>
    /// Ends the second half (regulation time).
    /// </summary>
    public MatchClock EndSecondHalf() => new(MatchPeriod.FullTime, ElapsedSeconds);

    /// <summary>
    /// Creates a match clock at a specific point in time (for reconstructing state).
    /// </summary>
    /// <param name="period">The match period.</param>
    /// <param name="elapsedSeconds">The elapsed seconds in that period.</param>
    /// <returns>A new MatchClock instance.</returns>
    public static MatchClock FromState(MatchPeriod period, int elapsedSeconds)
    {
        if (elapsedSeconds < 0)
        {
            throw new ArgumentException("Elapsed seconds cannot be negative.", nameof(elapsedSeconds));
        }

        return new MatchClock(period, elapsedSeconds);
    }
}
