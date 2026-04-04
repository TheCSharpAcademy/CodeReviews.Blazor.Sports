using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Domain.Tests.Teamsheets.TestData;

namespace SportsStatistics.Domain.Tests.Teamsheets;

public class TeamsheetPlayerTests
{
    [Fact]
    public void CreateStarter_ShouldCreateTeamsheetPlayer_WhenParametersAreValid()
    {
        // Arrange.
        var teamsheetId = Guid.NewGuid();
        var playerId = Guid.NewGuid();

        // Act.
        var teamsheetPlayer = TeamsheetPlayer.CreateStarter(teamsheetId, playerId);

        // Assert.
        teamsheetPlayer.ShouldNotBeNull();
        teamsheetPlayer.Id.ShouldNotBe(default);
        teamsheetPlayer.Id.Version.ShouldBe(7);
        teamsheetPlayer.TeamsheetId.ShouldBe(teamsheetId);
        teamsheetPlayer.PlayerId.ShouldBe(playerId);
        teamsheetPlayer.IsStarter.ShouldBeTrue();
    }

    [Fact]
    public void CreateSubstitute_ShouldCreateTeamsheetPlayer_WhenParametersAreValid()
    {
        // Arrange.
        var teamsheetId = Guid.NewGuid();
        var playerId = Guid.NewGuid();

        // Act.
        var teamsheetPlayer = TeamsheetPlayer.CreateSubstitute(teamsheetId, playerId);

        // Assert.
        teamsheetPlayer.ShouldNotBeNull();
        teamsheetPlayer.Id.ShouldNotBe(default);
        teamsheetPlayer.Id.Version.ShouldBe(7);
        teamsheetPlayer.TeamsheetId.ShouldBe(teamsheetId);
        teamsheetPlayer.PlayerId.ShouldBe(playerId);
        teamsheetPlayer.IsStarter.ShouldBeFalse();
    }
}