using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.GetByDate;

namespace SportsStatistics.Application.Tests.Fixtures.GetByDate;

public class GetFixturesByDateQueryValidatorTests
{
    public static readonly GetFixturesByDateQuery BaseCommand = new(DateOnly.FromDateTime(DateTime.UtcNow));

    private readonly GetFixturesByDateQueryValidator _validator;

    public GetFixturesByDateQueryValidatorTests()
    {
        _validator = new GetFixturesByDateQueryValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFixtureDateIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { FixtureDate = default };
        var expected = GetFixturesByDateQueryErrors.FixtureDateIsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureDate)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }
}
