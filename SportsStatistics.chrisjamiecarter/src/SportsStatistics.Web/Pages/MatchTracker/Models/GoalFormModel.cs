namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed class GoalFormModel
{
    public GoalType GoalType { get; set; }

    public bool IsOwnGoal { get; set; }

    public PlayerOptionDto? GoalScorer { get; set; }

    public PlayerOptionDto? Assister { get; set; }
}
