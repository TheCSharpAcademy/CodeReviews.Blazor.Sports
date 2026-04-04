using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Competitions.TestCases;

namespace SportsStatistics.Domain.Tests.Competitions;

public class CompetitionTests
{
    [Theory]
    [ClassData(typeof(CreateCompetitionTestCase))]
    public void Create_ShouldCreateCompetition_WhenParametersAreValid(Season season, Name name, Format format)
    {
        // Arrange.
        // Act.
        var competition = Competition.Create(season,
                                             name,
                                             format);

        // Assert.
        competition.ShouldNotBeNull();
        competition.Id.ShouldNotBe(default);
        competition.Id.Version.ShouldBe(7);
        competition.SeasonId.ShouldBe(season.Id);
        competition.Name.ShouldBe(name);
        competition.Format.ShouldBe(format);
        competition.Deleted.ShouldBeFalse();
        competition.DeletedOnUtc.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(CompetitionCreatedDomainEventTestCase))]
    public void Create_ShouldRaiseCompetitionCreatedDomainEvent_WhenCreatingCompetition(Season season, Name name, Format format)
    {
        // Arrange.
        // Act.
        var competition = Competition.Create(season,
                                             name,
                                             format);
        // Assert.
        competition.DomainEvents.ShouldHaveSingleItem()
                                .ShouldBeEquivalentTo(new CompetitionCreatedDomainEvent(competition.Id));
    }

    [Theory]
    [ClassData(typeof(ChangeNameDifferentTestCase))]
    public void ChangeName_ShouldChangeName_WhenNameIsDifferent(Competition competition, Name name)
    {
        // Arrange.
        // Act.
        var result = competition.ChangeName(name);

        // Assert.
        result.ShouldBeTrue();
        competition.Name.ShouldBeEquivalentTo(name);
    }

    [Theory]
    [ClassData(typeof(CompetitionNameChangedDomainEventTestCase))]
    public void ChangeName_ShouldRaiseCompetitionNameChangedDomainEvent_WhenNameIsChanged(Competition competition, Name name, CompetitionNameChangedDomainEvent expected)
    {
        // Arrange.
        competition.ClearDomainEvents();

        // Act.
        competition.ChangeName(name);

        // Assert.
        competition.DomainEvents.ShouldHaveSingleItem()
                                .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeNameIdenticalTestCase))]
    public void ChangeName_ShouldNotChangeName_WhenNameIsIdentical(Competition competition, Name name)
    {
        // Arrange.
        // Act.
        var result = competition.ChangeName(name);

        // Assert.
        result.ShouldBeFalse();
        competition.Name.ShouldBeEquivalentTo(name);
    }

    [Theory]
    [ClassData(typeof(ChangeFormatDifferentTestCase))]
    public void ChangeFormat_ShouldChangeFormat_WhenFormatIsDifferent(Competition competition, Format format)
    {
        // Arrange.
        // Act.
        var result = competition.ChangeFormat(format);

        // Assert.
        result.ShouldBeTrue();
        competition.Format.ShouldBeEquivalentTo(format);
    }

    [Theory]
    [ClassData(typeof(CompetitionFormatChangedDomainEventTestCase))]
    public void ChangeFormat_ShouldRaiseCompetitionFormatChangedDomainEvent_WhenFormatIsChanged(Competition competition, Format format, CompetitionFormatChangedDomainEvent expected)
    {
        // Arrange.
        competition.ClearDomainEvents();

        // Act.
        competition.ChangeFormat(format);

        // Assert.
        competition.DomainEvents.ShouldHaveSingleItem()
                                .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeFormatIdenticalTestCase))]
    public void ChangeFormat_ShouldNotChangeFormat_WhenFormatIsIdentical(Competition competition, Format format)
    {
        // Arrange.
        // Act.
        var result = competition.ChangeFormat(format);

        // Assert.
        result.ShouldBeFalse();
        competition.Format.ShouldBeEquivalentTo(format);
    }

    [Theory]
    [ClassData(typeof(DeleteCompetitionTestCase))]
    public void Delete_ShouldDeleteCompetition_WhenCompetitionIsNotAlreadyDeleted(Competition competition, DateTime utcNow)
    {
        // Arrange.
        // Act.
        competition.Delete(utcNow);

        // Assert.
        competition.Deleted.ShouldBeTrue();
        competition.DeletedOnUtc.ShouldBeEquivalentTo(utcNow);
    }

    [Theory]
    [ClassData(typeof(CompetitionDeletedDomainEventTestCase))]
    public void Delete_ShouldRaiseCompetitionDeletedDomainEvent_WhenCompetitionIsDeleted(Competition competition, DateTime utcNow, CompetitionDeletedDomainEvent expected)
    {
        // Arrange.
        competition.ClearDomainEvents();

        // Act.
        competition.Delete(utcNow);

        // Assert.
        competition.DomainEvents.ShouldHaveSingleItem()
                                .ShouldBeEquivalentTo(expected);
    }
}
