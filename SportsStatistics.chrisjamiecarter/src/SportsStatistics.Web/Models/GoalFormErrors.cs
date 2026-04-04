using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Models;

public static class GoalFormErrors
{
    public static Error GoalTypeRequired => Error.Validation(
        "GoalForm.GoalTypeRequired",
        "The goal type is required.");

    public static Error IsOwnGoalRequired => Error.Validation(
        "GoalForm.IsOwnGoalRequired",
        "The own goal flag is required.");

    public static Error GoalScorerRequired => Error.Validation(
        "GoalForm.GoalScorerRequired",
        "The goal scorer is required.");

    public static Error AssisterCannotBeGoalScorer => Error.Validation(
        "GoalForm.AssisterCannotBeGoalScorer",
        "The assister cannot be the same as the goal scorer.");
}