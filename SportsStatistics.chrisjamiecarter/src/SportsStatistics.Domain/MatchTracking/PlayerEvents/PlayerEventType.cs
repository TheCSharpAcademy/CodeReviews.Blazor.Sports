using System.ComponentModel.DataAnnotations;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.PlayerEvents;

public sealed class PlayerEventType : Enumeration<PlayerEventType>
{
    [Display(Name = "Pass Success")]
    public static readonly PlayerEventType PassSuccess = new(0, nameof(PassSuccess));

    [Display(Name = "Pass Failure")]
    public static readonly PlayerEventType PassFailure = new(1, nameof(PassFailure));

    [Display(Name = "Shot On Target")]
    public static readonly PlayerEventType ShotOnTarget = new(2, nameof(ShotOnTarget));

    [Display(Name = "Shot Off Target")]
    public static readonly PlayerEventType ShotOffTarget = new(3, nameof(ShotOffTarget));

    public static readonly PlayerEventType Goal = new(4, nameof(Goal));

    [Display(Name = "Goal Assist")]
    public static readonly PlayerEventType GoalAssist = new(5, nameof(GoalAssist));

    [Display(Name = "Own Goal")]
    public static readonly PlayerEventType OwnGoal = new(6, nameof(OwnGoal));

    public static readonly PlayerEventType Save = new(7, nameof(Save));

    public static readonly PlayerEventType Tackle = new(8, nameof(Tackle));

    [Display(Name = "Foul Won")]
    public static readonly PlayerEventType FoulWon = new(9, nameof(FoulWon));

    [Display(Name = "Foul Conceded")]
    public static readonly PlayerEventType FoulConceded = new(10, nameof(FoulConceded));

    [Display(Name = "Yellow Card")]
    public static readonly PlayerEventType YellowCard = new(11, nameof(YellowCard));

    [Display(Name = "Red Card")]
    public static readonly PlayerEventType RedCard = new(12, nameof(RedCard));

    private PlayerEventType(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEventType"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private PlayerEventType() { }
}
