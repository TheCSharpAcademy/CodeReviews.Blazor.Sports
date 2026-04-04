using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.MatchTracking.MatchEvents;

namespace SportsStatistics.Infrastructure.Tests.MatchTracking.MatchEvents;

public class MatchEventConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public MatchEventConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new MatchEventConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(MatchEvent))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(MatchEvent)}' entity type.");
    }

    [Fact]
    public void MatchEventConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.MatchEvents.Schema;
        var expectedTable = Schemas.MatchEvents.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(MatchEvent.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(MatchEvent.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureFixtureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(MatchEvent.FixtureId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.FixtureId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureMinutePropertyCorrectly()
    {
        // Arrange.
        var expectedBaseMinuteColumnName = nameof(MatchEvent.Minute.BaseMinute);
        var expectedStoppageMinuteColumnName = nameof(MatchEvent.Minute.StoppageMinute);

        // Act.
        var navigation = _entity.FindNavigation(nameof(MatchEvent.Minute));

        // Assert.
        navigation.ShouldNotBeNull();
        navigation.IsOnDependent.ShouldBeFalse(); // OwnsOne - navigation is on the principal

        var ownedEntityType = navigation.TargetEntityType;
        ownedEntityType.ShouldNotBeNull();

        var baseMinuteProperty = ownedEntityType.FindProperty(nameof(Minute.BaseMinute));
        baseMinuteProperty.ShouldNotBeNull();
        baseMinuteProperty.GetColumnName().ShouldBeEquivalentTo(expectedBaseMinuteColumnName);
        baseMinuteProperty.IsNullable.ShouldBe(false);

        var stoppageMinuteProperty = ownedEntityType.FindProperty(nameof(Minute.StoppageMinute));
        stoppageMinuteProperty.ShouldNotBeNull();
        stoppageMinuteProperty.GetColumnName().ShouldBeEquivalentTo(expectedStoppageMinuteColumnName);
        stoppageMinuteProperty.IsNullable.ShouldBe(true);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureOccurredAtUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(MatchEvent.OccurredAtUtc);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.OccurredAtUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureTypePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(MatchEvent.Type);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.Type));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(MatchEvent.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(MatchEvent.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(MatchEvent.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void MatchEventConfiguration_ShouldConfigureFixtureIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(MatchEvent.FixtureId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
