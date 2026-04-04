using FluentValidation.TestHelper;
using SportsStatistics.Domain.Players;
using SportsStatistics.Web.Pages.Admin.Players.Models;

namespace SportsStatistics.Web.Tests.Players;

public class PlayerFormModelValidatorTests
{
    private readonly PlayerFormModelValidator _validator;

    public PlayerFormModelValidatorTests()
    {
        _validator = new PlayerFormModelValidator();
    }

    [Fact]
    public void Validate_ShouldNotHaveAnyValidationErrors_WhenModelIsValid()
    {
        // Arrange.
        var model = new PlayerFormModel
        {
            Name = "Test Player",
            SquadNumber = 10,
            Nationality = "English",
            DateOfBirth = DateTime.UtcNow.AddYears(-20),
            Position = new PositionOptionDto(1, "Goalkeeper"),
            LeftClub = false
        };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNameIsEmpty()
    {
        // Arrange.
        var model = new PlayerFormModel { Name = string.Empty };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Name)
               .WithErrorCode(PlayerErrors.NameIsRequired.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNameExceedsMaxLength()
    {
        // Arrange.
        var model = new PlayerFormModel { Name = new string('A', Name.MaxLength + 1) };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Name)
               .WithErrorCode(PlayerErrors.NameExceedsMaxLength.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenSquadNumberIsBelowMin()
    {
        // Arrange.
        var model = new PlayerFormModel { SquadNumber = SquadNumber.MinValue - 1 };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.SquadNumber)
               .WithErrorCode(PlayerErrors.SquadNumberOutsideRange.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNationalityIsEmpty()
    {
        // Arrange.
        var model = new PlayerFormModel { Nationality = string.Empty };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Nationality)
               .WithErrorCode(PlayerErrors.NationalityIsRequired.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenDateOfBirthIsNull()
    {
        // Arrange.
        var model = new PlayerFormModel { DateOfBirth = null };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
               .WithErrorCode(PlayerErrors.DateOfBirthIsRequired.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenPositionIsNull()
    {
        // Arrange.
        var model = new PlayerFormModel { Position = null };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Position)
               .WithErrorCode(PlayerErrors.PositionNotFound.Code);
    }
}
