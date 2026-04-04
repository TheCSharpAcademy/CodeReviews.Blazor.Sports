namespace SportsStatistics.Domain.MatchTracking;

public static class MatchMinuteCalculator
{
    public static Minute Calculate(
        DateTime occuredAtUtc,
        DateTime firstHalfStartedAtUtc,
        DateTime? secondHalfStartedAtUtc,
        MatchPeriod matchPeriod)
    {
        if (matchPeriod == MatchPeriod.PreMatch || matchPeriod == MatchPeriod.FirstHalf)
        {
            return Calculate(occuredAtUtc - firstHalfStartedAtUtc, Minute.FirstHalfEndMinute);
        }

        if (matchPeriod == MatchPeriod.HalfTime || matchPeriod == MatchPeriod.SecondHalf)
        {
            if (secondHalfStartedAtUtc is null)
            {
                secondHalfStartedAtUtc = occuredAtUtc;
            }

            return Calculate(occuredAtUtc.AddMinutes(Minute.FirstHalfEndMinute) - secondHalfStartedAtUtc.Value, Minute.SecondHalfEndMinute);
        }

        throw new InvalidOperationException($"Cannot calculate minute for match period {matchPeriod}.");
    }
    
    private static Minute Calculate(
        TimeSpan elapsed,
        int baseEndMinute)
    {
        var elapsedMinutes = (int)elapsed.TotalMinutes + 1;
        if (elapsedMinutes <= baseEndMinute)
        {
            return Minute.Create(elapsedMinutes).Value;
        }
        else
        {
            var stoppageMinutes = elapsedMinutes - baseEndMinute;
            return Minute.Create(baseEndMinute, stoppageMinutes).Value;
        }
    }
}
