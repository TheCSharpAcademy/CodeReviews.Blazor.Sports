using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestCases;

namespace SportsStatistics.Domain.Tests.Seasons;

public class SeasonTests
{
    [Theory]
    [ClassData(typeof(CreateSeasonTestCase))]
    public void Create_ShouldCreateSeason_WhenParametersAreValid(DateRange dateRange)
    {
        // Arrange.
        // Act.
        var season = Season.Create(dateRange);

        // Assert.
        season.ShouldNotBeNull();
        season.Id.ShouldNotBe(default);
        season.Id.Version.ShouldBe(7);
        season.DateRange.ShouldBe(dateRange);
        season.Deleted.ShouldBeFalse();
        season.DeletedOnUtc.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(SeasonCreatedDomainEventTestCase))]
    public void Create_ShouldRaiseSeasonCreatedDomainEvent_WhenCreatingSeason(DateRange dateRange)
    {
        // Arrange.
        // Act.
        var season = Season.Create(dateRange);

        // Assert.
        season.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(new SeasonCreatedDomainEvent(season.Id));
    }

    [Theory]
    [ClassData(typeof(ChangeDateRangeDifferentTestCase))]
    public void Create_ShouldChangeDateRange_WhenDateRangeIsDifferent(Season season, DateRange dateRange)
    {
        // Arrange.
        // Act.
        var result = season.ChangeDateRange(dateRange);

        // Assert.
        result.ShouldBeTrue();
        season.DateRange.ShouldBeEquivalentTo(dateRange);
    }

    [Theory]
    [ClassData(typeof(SeasonDateRangeChangedDomainEventTestCase))]
    public void ChangeDateRange_ShouldRaiseSeasonDateRangeChangedDomainEvent_WhenDateRangeIsChanged(Season season, DateRange dateRange, SeasonDateRangeChangedDomainEvent expected)
    {
        // Arrange.
        season.ClearDomainEvents();

        // Act.
        season.ChangeDateRange(dateRange);

        // Assert.
        season.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeDateRangeIdenticalTestCase))]
    public void Create_ShouldNotChangeDateRange_WhenDateRangeIsIdentical(Season season, DateRange dateRange)
    {
        // Arrange.
        // Act.
        var result = season.ChangeDateRange(dateRange);

        // Assert.
        result.ShouldBeFalse();
        season.DateRange.ShouldBeEquivalentTo(dateRange);
    }

    [Theory]
    [ClassData(typeof(DeleteSeasonTestCase))]
    public void Delete_ShouldDeleteSeason_WhenSeasonIsNotAlreadyDeleted(Season season, DateTime utcNow)
    {
        // Arrange.
        // Act.
        season.Delete(utcNow);

        // Assert.
        season.Deleted.ShouldBeTrue();
        season.DeletedOnUtc.ShouldBeEquivalentTo(utcNow);
    }

    [Theory]
    [ClassData(typeof(SeasonDeletedDomainEventTestCase))]
    public void Delete_ShouldRaiseSeasonDeletedDomainEvent_WhenPlayerIsDeleted(Season season, DateTime utcNow, SeasonDeletedDomainEvent expected)
    {
        // Arrange.
        season.ClearDomainEvents();

        // Act.
        season.Delete(utcNow);

        // Assert.
        season.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }
}
