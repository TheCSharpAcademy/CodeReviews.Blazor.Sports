using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.MatchTracking.PlayerEvents;

namespace SportsStatistics.Infrastructure.Tests.MatchTracking.PlayerEvents;

public class PlayerEventConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public PlayerEventConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new PlayerEventConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(PlayerEvent))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(PlayerEvent)}' entity type.");
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.PlayerEvents.Schema;
        var expectedTable = Schemas.PlayerEvents.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(PlayerEvent.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureFixtureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.FixtureId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.FixtureId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureMinutePropertyCorrectly()
    {
        // Arrange.
        var expectedBaseMinuteColumnName = nameof(PlayerEvent.Minute.BaseMinute);
        var expectedStoppageMinuteColumnName = nameof(PlayerEvent.Minute.StoppageMinute);

        // Act.
        var navigation = _entity.FindNavigation(nameof(PlayerEvent.Minute));

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
    public void PlayerEventConfiguration_ShouldConfigureOccurredAtUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.OccurredAtUtc);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.OccurredAtUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigurePlayerIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.PlayerId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.PlayerId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureTypePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.Type);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.Type));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(PlayerEvent.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(PlayerEvent.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigureFixtureIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(PlayerEvent.FixtureId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void PlayerEventConfiguration_ShouldConfigurePlayerIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(PlayerEvent.PlayerId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
