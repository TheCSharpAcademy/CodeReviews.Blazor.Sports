using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.Infrastructure.Clubs;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Tests.Clubs;

public class ClubConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public ClubConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new ClubConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Club))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Club)}' entity type.");
    }

    [Fact]
    public void ClubConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Clubs.Schema;
        var expectedTable = Schemas.Clubs.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void ClubConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Club.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ClubConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Club.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Club.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void ClubConfiguration_ShouldConfigureNamePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Club.Name);
        var expectedIsNullable = false;
        var expectedMaxLength = Name.MaxLength;

        // Act.
        var complex = _entity.GetComplexProperties()
                    .Single(p => p.Name == nameof(Club.Name));

        var valueProperty = complex.ComplexType.FindProperty(nameof(Club.Name.Value));

        // Assert.
        valueProperty.ShouldNotBeNull();
        valueProperty.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        valueProperty.IsNullable.ShouldBe(expectedIsNullable);
        valueProperty.GetMaxLength().ShouldBe(expectedMaxLength);
    }
}