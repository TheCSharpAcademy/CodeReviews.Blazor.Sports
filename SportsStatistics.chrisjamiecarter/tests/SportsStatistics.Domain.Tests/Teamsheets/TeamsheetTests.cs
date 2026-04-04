using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Domain.Tests.Teamsheets.TestCases;
using SportsStatistics.Domain.Tests.Teamsheets.TestData;

namespace SportsStatistics.Domain.Tests.Teamsheets;

public class TeamsheetTests
{
    [Theory]
    [ClassData(typeof(CreateTeamsheetTestCase))]
    public void Create_ShouldCreateTeamsheet_WhenParametersAreValid(Guid fixtureId, DateTime submittedAtUtc)
    {
        // Arrange.
        // Act.
        var teamsheet = Teamsheet.Create(fixtureId, submittedAtUtc);

        // Assert.
        teamsheet.ShouldNotBeNull();
        teamsheet.Id.ShouldNotBe(default);
        teamsheet.Id.Version.ShouldBe(7);
        teamsheet.FixtureId.ShouldBe(fixtureId);
        teamsheet.SubmittedAtUtc.ShouldBe(submittedAtUtc);
        teamsheet.Deleted.ShouldBeFalse();
        teamsheet.DeletedOnUtc.ShouldBeNull();
        teamsheet.Players.ShouldBeEmpty();
    }

    [Fact]
    public void Create_ShouldRaiseTeamsheetCreatedDomainEvent_WhenCreatingTeamsheet()
    {
        // Arrange.
        // Act.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);

        // Assert.
        teamsheet.DomainEvents.ShouldHaveSingleItem()
                                .ShouldBeOfType<TeamsheetCreatedDomainEvent>();
    }

    [Fact]
    public void AddStarter_ShouldAddPlayerToStarters_WhenPlayerIsValid()
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
        var playerId = Guid.NewGuid();

        // Act.
        teamsheet.AddStarter(playerId);

        // Assert.
        teamsheet.Players.ShouldHaveSingleItem();
        teamsheet.Players.Single().IsStarter.ShouldBeTrue();
        teamsheet.Players.Single().PlayerId.ShouldBe(playerId);
    }

    [Fact]
    public void AddSubstitute_ShouldAddPlayerToSubstitutes_WhenPlayerIsValid()
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
        var playerId = Guid.NewGuid();

        // Act.
        teamsheet.AddSubstitute(playerId);

        // Assert.
        teamsheet.Players.ShouldHaveSingleItem();
        teamsheet.Players.Single().IsStarter.ShouldBeFalse();
        teamsheet.Players.Single().PlayerId.ShouldBe(playerId);
    }

    [Fact]
    public void GetStarterIds_ShouldReturnStarterPlayerIds()
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
        var starterId = Guid.NewGuid();
        teamsheet.AddStarter(starterId);

        // Act.
        var starterIds = teamsheet.GetStarterIds();

        // Assert.
        starterIds.ShouldHaveSingleItem();
        starterIds.ShouldContain(starterId);
    }

    [Fact]
    public void GetSubstituteIds_ShouldReturnSubstitutePlayerIds()
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
        var substituteId = Guid.NewGuid();
        teamsheet.AddSubstitute(substituteId);

        // Act.
        var substituteIds = teamsheet.GetSubstituteIds();

        // Assert.
        substituteIds.ShouldHaveSingleItem();
        substituteIds.ShouldContain(substituteId);
    }

    [Theory]
    [ClassData(typeof(DeleteTeamsheetTestCase))]
    public void Delete_ShouldSoftDeleteTeamsheet_WhenCalled(DateTime utcNow)
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);

        // Act.
        teamsheet.Delete(utcNow);

        // Assert.
        teamsheet.Deleted.ShouldBeTrue();
        teamsheet.DeletedOnUtc.ShouldBeEquivalentTo(utcNow);
    }

    [Fact]
    public void Delete_ShouldNotRaiseDomainEvent_WhenAlreadyDeleted()
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
        teamsheet.Delete(TeamsheetTestData.ValidUtcNow);
        teamsheet.ClearDomainEvents();

        // Act.
        teamsheet.Delete(TeamsheetTestData.ValidUtcNow);

        // Assert.
        teamsheet.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void Delete_ShouldRaiseTeamsheetDeletedDomainEvent_WhenDeleted()
    {
        // Arrange.
        var teamsheet = Teamsheet.Create(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
        teamsheet.ClearDomainEvents();

        // Act.
        teamsheet.Delete(TeamsheetTestData.ValidUtcNow);

        // Assert.
        teamsheet.DomainEvents.ShouldHaveSingleItem()
                                .ShouldBeOfType<TeamsheetDeletedDomainEvent>();
    }
}