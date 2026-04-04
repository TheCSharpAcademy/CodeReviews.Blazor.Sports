using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.Create;

public class CreateFixtureCommandValidatorTests
{
    private static readonly CreateFixtureCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                   "Test Opponent",
                                                                   DateTime.UtcNow,
                                                                   Location.Home.Value);

    private readonly CreateFixtureCommandValidator _validator;

    public CreateFixtureCommandValidatorTests()
    {
        _validator = new CreateFixtureCommandValidator();
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
        var expected = FixtureErrors.CompetitionId.IsRequired;

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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenOpponentIsEmpty(string opponent)
    {
        // Arrange.
        var command = BaseCommand with { Opponent = opponent };
        var expected = FixtureErrors.Opponent.IsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Opponent)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenOpponentExceedsMaximumLength()
    {
        // Arrange.
        var command = BaseCommand with { Opponent = new string('a', Opponent.MaxLength + 1) };
        var expected = FixtureErrors.Opponent.ExceedsMaxLength;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Opponent)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenKickoffTimeUtcIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = default };
        var expected = FixtureErrors.KickoffTimeUtc.IsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.KickoffTimeUtc)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenLocationIdIsValid()
    {
        foreach (var location in Location.List)
        {
            // Arrange.
            var command = BaseCommand with { LocationId = location.Value };

            // Act.
            var result = await _validator.TestValidateAsync(command);

            // Assert.
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenLocationIdIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { LocationId = -1 };
        var expected = FixtureErrors.Location.NotFound;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.LocationId)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }
}
