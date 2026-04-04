using FluentValidation.TestHelper;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Seasons.Create;

public class CreateSeasonCommandValidatorTests
{
    private static readonly List<Season> BaseSeasons = SeasonBuilder.GetDefaults();

    private static readonly CreateSeasonCommand BaseCommand = new(BaseSeasons.Last().DateRange.StartDate.AddYears(1),
                                                                  BaseSeasons.Last().DateRange.EndDate.AddYears(1));

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateSeasonCommandValidator _validator;

    public CreateSeasonCommandValidatorTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
              .ReturnsAsync(1);

        _validator = new CreateSeasonCommandValidator(_dbContextMock.Object);
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
    public async Task ValidateAsync_ShouldHaveValidationError_WhenStartDateIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { StartDate = default };
        var expected = "'Start Date' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StartDate)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenStartDateIsNotLessThanEndDate()
    {
        // Arrange.
        var command = BaseCommand with
        {
            StartDate = BaseCommand.EndDate.AddDays(1)
        };
        var expected = $"'Start Date' must be less than '{command.EndDate:dd/MM/yyyy}'.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StartDate)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenStartDateOverlapsWithExisting()
    {
        // Arrange.
        var command = BaseCommand with
        {
            StartDate = BaseSeasons.First().DateRange.StartDate,
        };
        var expected = "'Start Date' overlaps with an existing season.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.StartDate)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenEndDateIsEmpty()
    {
        // Arrange.
        var command = BaseCommand with { EndDate = default };
        var expected = "'End Date' must not be empty.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenEndDateIsNotGreaterThanStartDate()
    {
        // Arrange.
        var command = BaseCommand with
        {
            EndDate = BaseCommand.StartDate.AddDays(-1)
        };
        var expected = $"'End Date' must be greater than '{command.StartDate:dd/MM/yyyy}'.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage(expected);
    }

    [Fact]
    public async Task ValidateAsync_ShouldHaveValidationError_WhenEndDateOverlapsWithExisting()
    {
        // Arrange.
        var command = BaseCommand with
        {
            EndDate = BaseSeasons.First().DateRange.EndDate,
        };
        var expected = "'End Date' overlaps with an existing season.";

        // Act.
        var result = await _validator.TestValidateAsync(command);

        // Assert.
        result.ShouldHaveValidationErrorFor(c => c.EndDate)
              .WithErrorMessage(expected);
    }
}
