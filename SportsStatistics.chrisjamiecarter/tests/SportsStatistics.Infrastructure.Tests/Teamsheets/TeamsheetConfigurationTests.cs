using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Teamsheets;

namespace SportsStatistics.Infrastructure.Tests.Teamsheets;

public class TeamsheetConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public TeamsheetConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new TeamsheetConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Teamsheet))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Teamsheet)}' entity type.");
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Teamsheets.Schema;
        var expectedTable = Schemas.Teamsheets.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Teamsheet.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Teamsheet.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Teamsheet.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureFixtureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Teamsheet.FixtureId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Teamsheet.FixtureId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureSubmittedAtUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Teamsheet.SubmittedAtUtc);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Teamsheet.SubmittedAtUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Teamsheet.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(Teamsheet.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Teamsheet.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Teamsheet.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigureUniqueIndexOnFixtureId()
    {
        // Arrange.

        // Act.
        var indexes = _entity.GetIndexes();

        // Assert.
        var fixtureIndex = indexes.ShouldHaveSingleItem();
        fixtureIndex.IsUnique.ShouldBeTrue();
        fixtureIndex.Properties.ShouldHaveSingleItem();
        fixtureIndex.Properties[0].Name.ShouldBeEquivalentTo(nameof(Teamsheet.FixtureId));
    }

    [Fact]
    public void TeamsheetConfiguration_ShouldConfigurePlayersRelationship()
    {
        // Arrange.

        // Act.
        var navigation = _entity.FindNavigation(nameof(Teamsheet.Players));

        // Assert.
        navigation.ShouldNotBeNull();
        navigation.IsCollection.ShouldBeTrue();
        navigation.ForeignKey.ShouldNotBeNull();
        navigation.ForeignKey.DeleteBehavior.ShouldBe(DeleteBehavior.Cascade);
    }
}