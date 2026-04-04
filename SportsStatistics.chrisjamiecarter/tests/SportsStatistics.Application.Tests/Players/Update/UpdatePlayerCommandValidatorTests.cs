using FluentValidation.TestHelper;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandValidatorTests
{
    private static readonly UpdatePlayerCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                  "Test Name",
                                                                  1,
                                                                  "Test Nationality",
                                                                  DateOnly.FromDateTime(DateTime.Today).AddYears(-15),
                                                                  Position.Goalkeeper.Value,
                                                                  false);

    private readonly UpdatePlayerCommandValidator _validator;

    public UpdatePlayerCommandValidatorTests()
    {
        _validator = new UpdatePlayerCommandValidator();
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPlayerIdIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { PlayerId = default };
        var expected = PlayerErrors.PlayerIdIsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PlayerId)
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
        var expected = PlayerErrors.NameIsRequired;

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
        var expected = PlayerErrors.NameExceedsMaxLength;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenSquadNumberIsOutsideRange(int squadNumber)
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = squadNumber };
        var expected = PlayerErrors.SquadNumberOutsideRange;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.SquadNumber)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNationalityIsEmpty(string nationality)
    {
        // Arrange.
        var command = BaseCommand with { Nationality = nationality };
        var expected = PlayerErrors.NationalityIsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenNationalityExceedsMaximumLength()
    {
        // Arrange.
        var command = BaseCommand with { Nationality = new string('a', Nationality.MaxLength + 1) };
        var expected = PlayerErrors.NationalityExceedsMaxLength;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.Nationality)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenDateOfBirthIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = default };
        var expected = PlayerErrors.DateOfBirthIsRequired;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenDateOfBirthIsBelowMinAge()
    {
        // Arrange.
        var command = BaseCommand with { DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-DateOfBirth.MinAge + 1)) };
        var expected = PlayerErrors.DateOfBirthBelowMinAge;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNotHaveAnyValidationErrors_WhenPositionIdIsValid()
    {
        foreach (var position in Position.List)
        {
            // Arrange.
            var command = BaseCommand with { PositionId = position.Value };

            // Act.
            var result = await _validator.TestValidateAsync(command);

            // Assert.
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenPositionIdIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { PositionId = -1 };
        var expected =PlayerErrors.PositionNotFound;

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.PositionId)
              .WithErrorCode(expected.Code)
              .WithErrorMessage(expected.Description);
    }
}
