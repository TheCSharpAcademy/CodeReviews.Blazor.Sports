using FluentValidation.TestHelper;
using SportsStatistics.Application.Clubs.Update;
using SportsStatistics.Domain.Clubs;

namespace SportsStatistics.Application.Tests.Clubs.Update;

public class UpdateClubCommandValidatorTests
{
    private static readonly Club BaseClub = ClubBuilder.GetDefaults().First();

    private static readonly UpdateClubCommand BaseCommand = new(BaseClub.Id, "Updated Club Name");

    private readonly UpdateClubCommandValidator _validator;

    public UpdateClubCommandValidatorTests()
    {
        _validator = new UpdateClubCommandValidator();
    }

    [Fact]
    public void Validate_ShouldNotHaveAnyValidationErrors_WhenCommandIsValid()
    {
        // Arrange.
        var command = BaseCommand;

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenClubIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { ClubId = default };
        var expected = "The club identifier is required.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.ClubId)
               .WithErrorMessage(expected);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNameIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { Name = string.Empty };
        var expected = "The club name is required.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
               .WithErrorMessage(expected);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNameExceedsMaxLength()
    {
        // Arrange.
        var command = BaseCommand with { Name = new string('A', Name.MaxLength + 1) };
        var expected = $"The club name must be {Name.MaxLength} characters or less.";

        // Act.
        var result = _validator.TestValidate(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
               .WithErrorMessage(expected);
    }
}