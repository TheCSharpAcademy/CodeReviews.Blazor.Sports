using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.GetBySeasonId;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.GetBySeasonId;

public class GetFixturesBySeasonIdQueryValidatorTests
{
    private static readonly GetFixturesBySeasonIdQuery BaseCommand = new(Guid.CreateVersion7());

    private readonly GetFixturesBySeasonIdQueryValidator _validator;

    public GetFixturesBySeasonIdQueryValidatorTests()
    {
        _validator = new GetFixturesBySeasonIdQueryValidator();
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
        var expected = FixtureErrors.SeasonIdIsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SeasonId)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }
}
