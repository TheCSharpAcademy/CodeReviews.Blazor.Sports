using FluentValidation.TestHelper;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Tests.Fixtures.Delete;

public sealed class DeleteFixtureCommandValidatorTests
{
    private static readonly DeleteFixtureCommand BaseCommand = new(Guid.CreateVersion7());

    private readonly DeleteFixtureCommandValidator _validator;

    public DeleteFixtureCommandValidatorTests()
    {
        _validator = new DeleteFixtureCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenFixtureIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { FixtureId = default };
        var expected = FixtureErrors.Id.IsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.FixtureId)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }
}
