using System.ComponentModel.DataAnnotations;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public class MatchEventType : Enumeration<MatchEventType>
{
    [Display(Name = "First Half Started")]
    public static readonly MatchEventType FirstHalfStarted = new(0, nameof(FirstHalfStarted));

    [Display(Name = "First Half Finished")]
    public static readonly MatchEventType FirstHalfFinished = new(1, nameof(FirstHalfFinished));
    
    [Display(Name = "Second Half Started")]
    public static readonly MatchEventType SecondHalfStarted = new(2, nameof(SecondHalfStarted));

    [Display(Name = "Second Half Finished")]
    public static readonly MatchEventType SecondHalfFinished = new(3, nameof(SecondHalfFinished));
    
    [Display(Name = "Home Goal")]
    public static readonly MatchEventType HomeGoal = new(4, nameof(HomeGoal));

    [Display(Name = "Away Goal")]
    public static readonly MatchEventType AwayGoal = new(5, nameof(AwayGoal));

    private MatchEventType(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MatchEventType"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private MatchEventType() { }
}
