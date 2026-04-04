using FluentValidation.TestHelper;
using SportsStatistics.Application.Teamsheets.Create;
using SportsStatistics.Domain.Teamsheets;

namespace SportsStatistics.Application.Tests.Teamsheets.Create;

public class CreateTeamsheetCommandValidatorTests
{
    private readonly CreateTeamsheetCommandValidator _validator;

    public CreateTeamsheetCommandValidatorTests()
    {
        _validator = new CreateTeamsheetCommandValidator();
    }

    [Fact]
    public void Validate_ShouldNotHaveAnyValidationErrors_WhenCommandIsValid()
    {
        // Arrange.
        var starterIds = Enumerable.Range(1, Teamsheet.RequiredNumberOfStarters)
            .Select(_ => Guid.NewGuid())
            .ToList();
        var command = new CreateTeamsheetCommand(Guid.NewGuid(), starterIds);

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenFixtureIdIsEmpty()
    {
        // Arrange.
        var starterIds = Enumerable.Range(1, Teamsheet.RequiredNumberOfStarters)
            .Select(_ => Guid.NewGuid())
            .ToList();
        var command = new CreateTeamsheetCommand(default, starterIds);
        var expected = "Fixture identifier is required.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureId)
               .WithErrorMessage(expected);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenStarterIdsIsEmpty()
    {
        // Arrange.
        var command = new CreateTeamsheetCommand(Guid.NewGuid(), new List<Guid>());
        var expected = "Starter identifiers are required.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StarterIds)
               .WithErrorMessage(expected);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenStarterIdsCountIsIncorrect()
    {
        // Arrange.
        var starterIds = Enumerable.Range(1, 5).Select(_ => Guid.NewGuid()).ToList();
        var command = new CreateTeamsheetCommand(Guid.NewGuid(), starterIds);
        var expected = $"Teamsheet must contain exactly {Teamsheet.RequiredNumberOfStarters} starters.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StarterIds)
               .WithErrorMessage(expected);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenStarterIdsContainsDuplicates()
    {
        // Arrange.
        var duplicateId = Guid.NewGuid();
        var starterIds = new List<Guid>
        {
            duplicateId,
            duplicateId,
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };
        var command = new CreateTeamsheetCommand(Guid.NewGuid(), starterIds);
        var expected = "A player cannot be selected more than once.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StarterIds)
               .WithErrorMessage(expected);
    }
}