using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Teamsheets;

namespace SportsStatistics.Infrastructure.Tests.Teamsheets;

public class TeamsheetPlayerConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public TeamsheetPlayerConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new TeamsheetPlayerConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(TeamsheetPlayer))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(TeamsheetPlayer)}' entity type.");
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.TeamsheetPlayers.Schema;
        var expectedTable = Schemas.TeamsheetPlayers.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(TeamsheetPlayer.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(TeamsheetPlayer.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(TeamsheetPlayer.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldConfigureTeamsheetIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(TeamsheetPlayer.TeamsheetId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(TeamsheetPlayer.TeamsheetId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldConfigurePlayerIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(TeamsheetPlayer.PlayerId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(TeamsheetPlayer.PlayerId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldConfigureIsStarterPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(TeamsheetPlayer.IsStarter);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(TeamsheetPlayer.IsStarter));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void TeamsheetPlayerConfiguration_ShouldConfigureUniqueIndexOnTeamsheetIdAndPlayerId()
    {
        // Arrange.

        // Act.
        var indexes = _entity.GetIndexes();

        // Assert.
        indexes.ShouldHaveSingleItem();
        var index = indexes.First();
        index.IsUnique.ShouldBeTrue();
        index.Properties.Count.ShouldBe(2);
        index.Properties.ShouldContain(p => p.Name == nameof(TeamsheetPlayer.TeamsheetId));
        index.Properties.ShouldContain(p => p.Name == nameof(TeamsheetPlayer.PlayerId));
    }
}