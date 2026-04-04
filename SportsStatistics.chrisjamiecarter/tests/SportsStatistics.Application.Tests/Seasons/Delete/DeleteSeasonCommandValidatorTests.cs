using FluentValidation.TestHelper;
using SportsStatistics.Application.Seasons.Delete;

namespace SportsStatistics.Application.Tests.Seasons.Delete;

public sealed class DeleteSeasonCommandValidatorTests
{
    private static readonly DeleteSeasonCommand BaseCommand = new(Guid.CreateVersion7());

    private readonly DeleteSeasonCommandValidator _validator;

    public DeleteSeasonCommandValidatorTests()
    {
        _validator = new DeleteSeasonCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = default };
        var expected = "'Season Id' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SeasonId)
              .WithErrorMessage(expected);
    }
}
