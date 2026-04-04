using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Fixtures;

namespace SportsStatistics.Infrastructure.Tests.Fixtures;

public class FixtureConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public FixtureConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new FixtureConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Fixture))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Fixture)}' entity type.");
    }

    [Fact]
    public void FixtureConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Fixtures.Schema;
        var expectedTable = Schemas.Fixtures.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Fixture.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureCompetitionIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.CompetitionId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.CompetitionId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureOpponentPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.Opponent);
        var expectedMaxLength = Opponent.MaxLength;
        var expectedIsNullable = false;

        // Act.
        var complex = _entity.GetComplexProperties()
                            .Single(p => p.Name == nameof(Fixture.Opponent));

        var property = complex.ComplexType.FindProperty(nameof(Opponent.Value));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureKickoffTimeUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.KickoffTimeUtc);
        var expectedIsNullable = false;

        // Act.
        var complex = _entity.GetComplexProperties()
                            .Single(p => p.Name == nameof(Fixture.KickoffTimeUtc));

        var property = complex.ComplexType.FindProperty(nameof(KickoffTimeUtc.Value));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureLocationPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.Location);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Location));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureScorePropertyCorrectly()
    {
        // Arrange.
        var expectedHomeGoalsColumnName = nameof(Fixture.Score.HomeGoals);
        var expectedAwayGoalsColumnName = nameof(Fixture.Score.AwayGoals);
        var expectedIsOwned = true;
        var expectedIsNullable = false;

        // Act.
        var ownedNavigation = _entity.FindNavigation(nameof(Fixture.Score));
        var ownedEntityType = ownedNavigation?.TargetEntityType;
        var homeGoalsProperty = ownedEntityType?.FindProperty(nameof(Score.HomeGoals));
        var awayGoalsProperty = ownedEntityType?.FindProperty(nameof(Score.AwayGoals));

        // Assert.
        ownedEntityType.ShouldNotBeNull();
        ownedEntityType.IsOwned().ShouldBe(expectedIsOwned);
        homeGoalsProperty.ShouldNotBeNull();
        homeGoalsProperty.GetColumnName().ShouldBeEquivalentTo(expectedHomeGoalsColumnName);
        homeGoalsProperty.IsNullable.ShouldBe(expectedIsNullable);
        awayGoalsProperty.ShouldNotBeNull();
        awayGoalsProperty.GetColumnName().ShouldBeEquivalentTo(expectedAwayGoalsColumnName);
        awayGoalsProperty.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureStatusPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.Status);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Status));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Fixture.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Fixture.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void FixtureConfiguration_ShouldConfigureCompetitionIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(Fixture.CompetitionId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
