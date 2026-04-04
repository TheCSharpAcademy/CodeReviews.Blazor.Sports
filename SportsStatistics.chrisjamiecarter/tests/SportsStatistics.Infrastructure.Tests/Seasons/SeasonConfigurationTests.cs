using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Seasons;

namespace SportsStatistics.Infrastructure.Tests.Seasons;

public class SeasonConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public SeasonConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new SeasonConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Season))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Season)}' entity type.");
    }

    [Fact]
    public void SeasonConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Seasons.Schema;
        var expectedTable = Schemas.Seasons.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Season.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Season.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Season.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureDateRangePropertyCorrectly()
    {
        // Arrange.
        var expectedStartDateColumnName = nameof(Season.DateRange.StartDate);
        var expectedEndDateColumnName = nameof(Season.DateRange.EndDate);
        var expectedIsNullable = false;

        // Act.
        var complex = _entity.GetComplexProperties()
                    .Single(p => p.Name == nameof(Season.DateRange));

        var startDateProperty = complex.ComplexType.FindProperty(nameof(Season.DateRange.StartDate));
        var endDateProperty = complex.ComplexType.FindProperty(nameof(Season.DateRange.EndDate));

        // Assert.
        startDateProperty.ShouldNotBeNull();
        startDateProperty.GetColumnName().ShouldBeEquivalentTo(expectedStartDateColumnName);
        startDateProperty.IsNullable.ShouldBe(expectedIsNullable);
        endDateProperty.ShouldNotBeNull();
        endDateProperty.GetColumnName().ShouldBeEquivalentTo(expectedEndDateColumnName);
        endDateProperty.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureDeletedOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Season.DeletedOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(Season.DeletedOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void SeasonConfiguration_ShouldConfigureDeletedPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Season.Deleted);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Season.Deleted));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void SeasonConfiguration_ShouldIgnoreName()
    {
        // Arrange.

        // Act.
        var property = _entity.FindProperty(nameof(Season.Name));

        // Assert.
        property.ShouldBeNull();
    }
}
