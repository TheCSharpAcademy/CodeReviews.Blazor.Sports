using System.Globalization;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

/// <summary>
/// Represents a match minute with optional stoppage time notation.
/// </summary>
public sealed record Minute
{
    public const int MinMinute = 1;
    public const int MaxBaseMinute = 90;
    public const int FirstHalfEndMinute = 45;
    public const int SecondHalfEndMinute = 90;

    private Minute(int baseMinute, int? stoppageMinute)
    {
        BaseMinute = baseMinute;
        StoppageMinute = stoppageMinute;
    }

    public int BaseMinute { get; }

    public int? StoppageMinute { get; }

    /// <summary>
    /// Gets the display notation for this minute (e.g., "45", "45+1", "90+2").
    /// </summary>
    public string Display => StoppageMinute.HasValue
        ? $"{BaseMinute.ToString(CultureInfo.InvariantCulture)}+{StoppageMinute.Value.ToString(CultureInfo.InvariantCulture)}"
        : BaseMinute.ToString(CultureInfo.InvariantCulture);

    public static Result<Minute> Create(int? baseMinute, int? stoppageMinute = null)
    {
        if (baseMinute is null)
        {
            return MinuteErrors.Null;
        }

        if (baseMinute < MinMinute)
        {
            return MinuteErrors.BelowMinimum;
        }

        if (baseMinute > MaxBaseMinute)
        {
            return MinuteErrors.AboveMaximum;
        }

        if (stoppageMinute.HasValue)
        {
            if (stoppageMinute.Value < MinMinute)
            {
                return MinuteErrors.InvalidStoppageMinute;
            }

            if (baseMinute is not FirstHalfEndMinute && baseMinute is not SecondHalfEndMinute)
            {
                return MinuteErrors.InvalidStoppageBaseMinute;
            }
        }

        return new Minute(baseMinute.Value, stoppageMinute);
    }
}
