namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed record GoalDialogData(
    GoalType GoalType,
    bool IsOwnGoal,
    IEnumerable<PlayerOptionDto> ScorerOptions,
    IEnumerable<PlayerOptionDto> AssisterOptions,
    Guid? ScorerPlayerId,
    Guid? AssistPlayerId,
    DateTime OccurredAtUtc);
