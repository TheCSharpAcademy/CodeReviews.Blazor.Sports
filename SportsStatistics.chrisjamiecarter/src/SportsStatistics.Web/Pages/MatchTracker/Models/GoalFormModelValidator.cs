using FluentValidation;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;
using SportsStatistics.Web.Models;

namespace SportsStatistics.Web.Pages.MatchTracker.Models;

internal sealed class GoalFormModelValidator : AbstractValidator<GoalFormModel>
{
    public GoalFormModelValidator()
    {
        RuleFor(model => model.GoalType)
            .IsInEnum().WithError(MatchEventBaseErrors.PlayerEventType.Unknown);

        RuleFor(model => model.IsOwnGoal)
            .NotNull().WithError(GoalFormErrors.IsOwnGoalRequired);

        RuleFor(model => model.GoalScorer)
            .NotEmpty()
            .WithError(PlayerEventErrors.PlayerIdIsRequired)
            .When(model =>
                (model.GoalType == GoalType.TeamGoal && !model.IsOwnGoal) ||
                (model.GoalType == GoalType.OppositionGoal && model.IsOwnGoal));

        RuleFor(model => model.Assister)
            .NotEqual(model => model.GoalScorer)
            .WithError(GoalFormErrors.AssisterCannotBeGoalScorer)
            .When(model => model.GoalScorer is not null && model.Assister is not null);
    }
}
