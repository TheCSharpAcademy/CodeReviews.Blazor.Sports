using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Create;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Tests.Competitions.Create;

public class CreateCompetitionCommandValidatorTests
{
    private static readonly CreateCompetitionCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                       "Test Name",
                                                                       Format.League.Value);

    private readonly CreateCompetitionCommandValidator _validator;

    public CreateCompetitionCommandValidatorTests()
    {
        _validator = new CreateCompetitionCommandValidator();
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenCommandIsValid()
    {
        // Arrange.
        var command = BaseCommand;

        // Act.
        var result = await _validator.TestValidateAsync(command);
        var valid = await _validator.ValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenSeasonIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = default };
        var expected = CompetitionErrors.SeasonId.IsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SeasonId)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameIsEmpty(string name)
    {
        // Arrange.
        var command = BaseCommand with { Name = name };
        var expected = CompetitionErrors.Name.IsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNameExceedsMaximumLength()
    {
        // Arrange.
        var command = BaseCommand with { Name = new string('a', Name.MaxLength + 1) };
        var expected = CompetitionErrors.Name.ExceedsMaxLength;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenFormatIdIsValid()
    {
        foreach (var format in Format.List)
        {
            // Arrange.
            var command = BaseCommand with { FormatId = format.Value };

            // Act.
            var result = await _validator.TestValidateAsync(command);

            // Assert.
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFormatIdIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { FormatId = -1 };
        var expected = CompetitionErrors.Format.NotFound;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FormatId)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }
}
