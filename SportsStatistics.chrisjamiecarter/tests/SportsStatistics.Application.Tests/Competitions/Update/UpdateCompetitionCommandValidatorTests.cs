using FluentValidation.TestHelper;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Tests.Competitions.Update;

public class UpdateCompetitionCommandValidatorTests
{
    private static readonly UpdateCompetitionCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                       "Test Name",
                                                                       Format.League.Value);

    private readonly UpdateCompetitionCommandValidator _validator;

    public UpdateCompetitionCommandValidatorTests()
    {
        _validator = new UpdateCompetitionCommandValidator();
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenCommandIsValid()
    {
        // Arrange.
        var command = BaseCommand;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenCompetitionIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = default };
        var expected = CompetitionErrors.Id.IsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.CompetitionId)
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
