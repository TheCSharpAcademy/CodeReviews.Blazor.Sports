using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Infrastructure.Competitions;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Tests.Competitions;

public class CompetitionConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public CompetitionConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new CompetitionConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Competition))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Competition)}' entity type.");
    }

    [Fact]
    public void CompetitionConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Competitions.Schema;
        var expectedTable = Schemas.Competitions.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Competition.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Competition.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureSeasonIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Competition.SeasonId);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.SeasonId));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureNamePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Competition.Name);
        var expectedMaxLength = Name.MaxLength;
        var expectedIsNullable = false;

        // Act.
        var complex = _entity.GetComplexProperties()
                             .Single(p => p.Name == nameof(Competition.Name));

        var property = complex.ComplexType.FindProperty(nameof(Name.Value));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureFormatPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Competition.Format);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.Format));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Competition.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Competition.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Competition.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void CompetitionConfiguration_ShouldConfigureSeasonIdAsForeignKey()
    {
        // Arrange.
        var expected = nameof(Competition.SeasonId);

        // Act.
        var foreignKey = _entity.GetForeignKeys()
                                .SingleOrDefault(fk => fk.Properties.Any(p => p.Name == expected));

        // Assert.
        foreignKey.ShouldNotBeNull();
        foreignKey.Properties.ShouldHaveSingleItem();
        foreignKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }
}
