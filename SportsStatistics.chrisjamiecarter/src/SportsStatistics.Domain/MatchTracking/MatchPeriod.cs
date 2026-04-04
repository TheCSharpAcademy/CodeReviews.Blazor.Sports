using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

/// <summary>
/// Represents the periods of a football match.
/// </summary>
public sealed class MatchPeriod : Enumeration<MatchPeriod>
{
    /// <summary>
    /// Before the match has started.
    /// </summary>
    public static readonly MatchPeriod PreMatch = new(0, nameof(PreMatch));

    /// <summary>
    /// First half of regulation time (minutes 1-45).
    /// Including stoppage time at the end of the first half (minutes 45+1, 45+2, etc.).
    /// </summary>
    public static readonly MatchPeriod FirstHalf = new(1, nameof(FirstHalf));

    /// <summary>
    /// Half-time interval between first and second half.
    /// </summary>
    public static readonly MatchPeriod HalfTime = new(3, nameof(HalfTime));

    /// <summary>
    /// Second half of regulation time (minutes 46-90).
    /// Including stoppage time at the end of the second half (minutes 90+1, 90+2, etc.).
    /// </summary>
    public static readonly MatchPeriod SecondHalf = new(4, nameof(SecondHalf));

    /// <summary>
    /// Full-time (end of regulation).
    /// </summary>
    public static readonly MatchPeriod FullTime = new(6, nameof(FullTime));

    /// <summary>
    /// Initializes a new instance of the <see cref="MatchPeriod"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private MatchPeriod() { }

    private MatchPeriod(int id, string name) : base(id, name) { }

    /// <summary>
    /// Gets the base minute for this period (the minute displayed before any stoppage notation).
    /// </summary>
    /// <returns>The base minute (45, 90, 105, or 120) for stoppage periods; otherwise null.</returns>
    //public int? GetStoppageBaseMinute() => this switch
    //{
    //    _ when this == FirstHalf => 45,
    //    _ when this == SecondHalf => 90,
    //    _ => null
    //};

    /// <summary>
    /// Determines whether this period allows match events to be recorded.
    /// </summary>
    public bool IsPlayingPeriod() 
        => this == FirstHalf
        || this == SecondHalf;

    public bool IsSubstitutionPeriod()
        => this == FirstHalf 
        || this == HalfTime
        || this == SecondHalf;
}
