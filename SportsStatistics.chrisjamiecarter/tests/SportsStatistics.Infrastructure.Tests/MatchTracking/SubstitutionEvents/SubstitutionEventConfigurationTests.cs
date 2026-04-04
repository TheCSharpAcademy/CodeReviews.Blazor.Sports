using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.MatchTracking.SubstitutionEvents;

namespace SportsStatistics.Infrastructure.Tests.MatchTracking.SubstitutionEvents;

public class SubstitutionEventConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public SubstitutionEventConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new SubstitutionEventConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(SubstitutionEvent))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(SubstitutionEvent)}' entity type.");
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.SubstitutionEvents.Schema;
        var expectedTable = Schemas.SubstitutionEvents.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(SubstitutionEvent.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBe(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureFixtureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.FixtureId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.FixtureId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBe(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureMinutePropertyCorrectly()
    {
        // Arrange.
        var expectedBaseMinuteColumnName = nameof(SubstitutionEvent.Minute.BaseMinute);
        var expectedStoppageMinuteColumnName = nameof(SubstitutionEvent.Minute.StoppageMinute);

        // Act.
        var navigation = _entity.FindNavigation(nameof(SubstitutionEvent.Minute));

        // Assert.
        navigation.ShouldNotBeNull();
        navigation.IsOnDependent.ShouldBeFalse(); // OwnsOne - navigation is on the principal

        var ownedEntityType = navigation.TargetEntityType;
        ownedEntityType.ShouldNotBeNull();

        var baseMinuteProperty = ownedEntityType.FindProperty(nameof(Minute.BaseMinute));
        baseMinuteProperty.ShouldNotBeNull();
        baseMinuteProperty.GetColumnName().ShouldBe(expectedBaseMinuteColumnName);
        baseMinuteProperty.IsNullable.ShouldBe(false);

        var stoppageMinuteProperty = ownedEntityType.FindProperty(nameof(Minute.StoppageMinute));
        stoppageMinuteProperty.ShouldNotBeNull();
        stoppageMinuteProperty.GetColumnName().ShouldBe(expectedStoppageMinuteColumnName);
        stoppageMinuteProperty.IsNullable.ShouldBe(true);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureOccurredAtUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.OccurredAtUtc);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.OccurredAtUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBe(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigurePlayerOffIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.Substitution.PlayerOffId);
        var expectedIsNullable = false;

        // Act.
        var ownedNavigation = _entity.FindNavigation(nameof(SubstitutionEvent.Substitution));
        var ownedEntityType = ownedNavigation?.TargetEntityType;
        var property = ownedEntityType?.FindProperty(nameof(Substitution.PlayerOffId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBe(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigurePlayerOnIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.Substitution.PlayerOnId);
        var expectedIsNullable = false;

        // Act.
        var ownedNavigation = _entity.FindNavigation(nameof(SubstitutionEvent.Substitution));
        var ownedEntityType = ownedNavigation?.TargetEntityType;
        var property = ownedEntityType?.FindProperty(nameof(Substitution.PlayerOnId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBe(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(SubstitutionEvent.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(SubstitutionEvent.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigurePlayerOffIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(SubstitutionEvent.Substitution.PlayerOffId);
        var expectedDeleteBehavior = DeleteBehavior.NoAction;

        // Act.
        var ownedNavigation = _entity.FindNavigation(nameof(SubstitutionEvent.Substitution));
        var ownedEntityType = ownedNavigation?.TargetEntityType;
        var foreignKey = ownedEntityType?.GetForeignKeys()
                                         .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
        foreignKey.DeleteBehavior.ShouldBeEquivalentTo(expectedDeleteBehavior);
    }

    [Fact]
    public void SubstitutionEventConfiguration_ShouldConfigurePlayerOnIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(SubstitutionEvent.Substitution.PlayerOffId);
        var expectedDeleteBehavior = DeleteBehavior.NoAction;

        // Act.
        var ownedNavigation = _entity.FindNavigation(nameof(SubstitutionEvent.Substitution));
        var ownedEntityType = ownedNavigation?.TargetEntityType;
        var foreignKey = ownedEntityType?.GetForeignKeys()
                                         .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
        foreignKey.DeleteBehavior.ShouldBeEquivalentTo(expectedDeleteBehavior);
    }
}
