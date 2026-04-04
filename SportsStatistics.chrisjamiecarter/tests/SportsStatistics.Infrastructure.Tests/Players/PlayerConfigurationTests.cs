using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Players;

namespace SportsStatistics.Infrastructure.Tests.Players;

public class PlayerConfigurationTests
{
    private readonly IMutableEntityType _entity;

    public PlayerConfigurationTests()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());

        _entity = modelBuilder.Model.FindEntityType(typeof(Player))
            ?? throw new InvalidOperationException($"Unable to find '{nameof(Player)}' entity type.");
    }

    [Fact]
    public void PlayerConfiguration_ShouldMapToCorrectTableAndSchema()
    {
        // Arrange.
        var expectedSchema = Schemas.Players.Schema;
        var expectedTable = Schemas.Players.Table;

        // Act.
        var resultSchema = _entity.GetSchema();
        var resultTable = _entity.GetTableName();

        // Assert.
        resultSchema.ShouldBeEquivalentTo(expectedSchema);
        resultTable.ShouldBeEquivalentTo(expectedTable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureIdAsPrimaryKey()
    {
        // Arrange.
        var expected = nameof(Player.Id);

        // Act.
        var primaryKey = _entity.FindPrimaryKey();

        // Assert.
        primaryKey.ShouldNotBeNull();
        primaryKey.Properties.ShouldHaveSingleItem();
        primaryKey.Properties[0].Name.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureIdPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.Id);
        var expectedIsNullable = false;
        var expectedValueGenerated = ValueGenerated.Never;

        // Act.
        var property = _entity.FindProperty(nameof(Player.Id));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.ValueGenerated.ShouldBe(expectedValueGenerated);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureNamePropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.Name);
        var expectedIsNullable = false;
        var expectedMaxLength = Name.MaxLength;

        // Act.
        var complex = _entity.GetComplexProperties()
                             .Single(p => p.Name == nameof(Player.Name));

        var property = complex.ComplexType.FindProperty(nameof(Name.Value));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureSquadNumberPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.SquadNumber);
        var expectedIsNullable = false;

        // Act.
        var complex = _entity.GetComplexProperties()
                             .Single(p => p.Name == nameof(Player.SquadNumber));

        var property = complex.ComplexType.FindProperty(nameof(SquadNumber.Value));


        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureNationalityPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.Nationality);
        var expectedIsNullable = false;
        var expectedMaxLength = Nationality.MaxLength;

        // Act.
        var complex = _entity.GetComplexProperties()
                             .Single(p => p.Name == nameof(Player.Nationality));

        var property = complex.ComplexType.FindProperty(nameof(Nationality.Value));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.GetMaxLength().ShouldBe(expectedMaxLength);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureDateOfBirthPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.DateOfBirth);
        var expectedIsNullable = false;

        // Act.
        var complex = _entity.GetComplexProperties()
                             .Single(p => p.Name == nameof(Player.DateOfBirth));

        var property = complex.ComplexType.FindProperty(nameof(DateOfBirth.Value));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigurePositionPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.Position);
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.Position));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureLeftClubPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.LeftClub);
        var expectedDefaultValue = false;
        var expectedIsNullable = false;

        // Act.
        var property = _entity.FindProperty(nameof(Player.LeftClub));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
        property.GetDefaultValue().ShouldBe(expectedDefaultValue);
    }

    [Fact]
    public void PlayerConfiguration_ShouldConfigureLeftClubOnUtcPropertyCorrectly()
    {
        // Arrange.
        var expectedColumnName = nameof(Player.LeftClubOnUtc);
        var expectedIsNullable = true;

        // Act.
        var property = _entity.FindProperty(nameof(Player.LeftClubOnUtc));

        // Assert.
        property.ShouldNotBeNull();
        property.GetColumnName().ShouldBeEquivalentTo(expectedColumnName);
        property.IsNullable.ShouldBe(expectedIsNullable);
    }

    [Fact]
    public void PlayerConfiguration_ShouldIgnoreAge()
    {
        // Arrange.

        // Act.
        var property = _entity.FindProperty(nameof(Player.Age));

        // Assert.
        property.ShouldBeNull();
    }
}
