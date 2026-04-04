using FluentValidation.TestHelper;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.Web.Pages.Admin.Clubs.Models;

namespace SportsStatistics.Web.Tests.Clubs;

public class ClubFormModelValidatorTests
{
    private readonly ClubFormModelValidator _validator;

    public ClubFormModelValidatorTests()
    {
        _validator = new ClubFormModelValidator();
    }

    [Fact]
    public void Validate_ShouldNotHaveAnyValidationErrors_WhenModelIsValid()
    {
        // Arrange.
        var model = new ClubFormModel { Name = "Test Club" };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNameIsEmpty()
    {
        // Arrange.
        var model = new ClubFormModel { Name = string.Empty };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Name)
               .WithErrorCode(ClubErrors.Name.IsRequired.Code);
    }

    [Fact]
    public void Validate_ShouldHaveValidationError_WhenNameExceedsMaxLength()
    {
        // Arrange.
        var model = new ClubFormModel { Name = new string('A', 256) };

        // Act.
        var result = _validator.TestValidate(model);

        // Assert.
        result.ShouldHaveValidationErrorFor(x => x.Name)
               .WithErrorCode(ClubErrors.Name.ExceedsMaxLength.Code);
    }
}
