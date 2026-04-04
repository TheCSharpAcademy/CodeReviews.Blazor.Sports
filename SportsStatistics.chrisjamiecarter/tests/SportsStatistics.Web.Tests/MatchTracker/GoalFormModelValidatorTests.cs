using FluentValidation.TestHelper;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Web.Models;
using SportsStatistics.Web.Pages.MatchTracker.Models;

namespace SportsStatistics.Web.Tests.MatchTracker;

public class GoalFormModelValidatorTests
{
    private readonly GoalFormModelValidator _validator;

    public GoalFormModelValidatorTests()
    {
        _validator = new GoalFormModelValidator();
    }

    [Fact]
    public void Validate_ShouldNotHaveAnyValidationErrors_WhenModelIsValid()
    {
        // Arrange.
        var model = new GoalFormModel
        {
            GoalType = GoalType.TeamGoal,
            IsOwnGoal = false,
            GoalScorer = new PlayerOptionDto(Guid.NewGuid(), "Test Player"),
            Assister = new PlayerOptionDto(Guid.NewGuid(), "Test Assister")
        };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenGoalScorerIsNull_ForTeamGoal()
    {
        // Arrange.
        var model = new GoalFormModel
        {
            GoalType = GoalType.TeamGoal,
            IsOwnGoal = false,
            GoalScorer = null
        };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.GoalScorer)
               .WithErrorCode(PlayerEventErrors.PlayerIdIsRequired.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenAssisterIsSameAsGoalScorer()
    {
        // Arrange.
        var sameId = Guid.NewGuid();
        var model = new GoalFormModel
        {
            GoalType = GoalType.TeamGoal,
            IsOwnGoal = false,
            GoalScorer = new PlayerOptionDto(sameId, "Test Player"),
            Assister = new PlayerOptionDto(sameId, "Test Player")
        };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Assister)
               .WithErrorCode(GoalFormErrors.AssisterCannotBeGoalScorer.Code);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenAssisterIsDifferentFromGoalScorer()
    {
        // Arrange.
        var model = new GoalFormModel
        {
            GoalType = GoalType.TeamGoal,
            IsOwnGoal = false,
            GoalScorer = new PlayerOptionDto(Guid.NewGuid(), "Goal Scorer"),
            Assister = new PlayerOptionDto(Guid.NewGuid(), "Assister")
        };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldNotHaveValidationErrorFor(x => x.Assister);
    }
}
