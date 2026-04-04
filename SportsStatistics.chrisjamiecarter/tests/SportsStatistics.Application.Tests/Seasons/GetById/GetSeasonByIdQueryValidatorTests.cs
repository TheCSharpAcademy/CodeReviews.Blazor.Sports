using FluentValidation.TestHelper;
using SportsStatistics.Application.Seasons.GetById;

namespace SportsStatistics.Application.Tests.Seasons.GetById;

public class GetSeasonByIdQueryValidatorTests
{
    private static readonly GetSeasonByIdQuery BaseCommand = new(Guid.CreateVersion7());

    private readonly GetSeasonByIdQueryValidator _validator;

    public GetSeasonByIdQueryValidatorTests()
    {
        _validator = new GetSeasonByIdQueryValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenSeasonIdIsEmpty()
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
