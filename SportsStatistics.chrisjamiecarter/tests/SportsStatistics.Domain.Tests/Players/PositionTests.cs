using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class PositionTests
{
    [Fact]
    public void Resolve_ShouldReturnGoalkeeper_WhenIdIsZero()
    {
        // Arrange.
        // Act.
        var result = Position.Resolve(0);

        // Assert.
        result.Value.ShouldBe(Position.Goalkeeper);
    }

    [Fact]
    public void Resolve_ShouldReturnDefender_WhenIdIsOne()
    {
        // Arrange.
        // Act.
        var result = Position.Resolve(1);

        // Assert.
        result.Value.ShouldBe(Position.Defender);
    }

    [Fact]
    public void Resolve_ShouldReturnMidfielder_WhenIdIsTwo()
    {
        // Arrange.
        // Act.
        var result = Position.Resolve(2);

        // Assert.
        result.Value.ShouldBe(Position.Midfielder);
    }

    [Fact]
    public void Resolve_ShouldReturnAttacker_WhenIdIsThree()
    {
        // Arrange.
        // Act.
        var result = Position.Resolve(3);

        // Assert.
        result.Value.ShouldBe(Position.Attacker);
    }

    [Fact]
    public void Resolve_ShouldReturnError_WhenIdIsInvalid()
    {
        // Arrange.
        var invalidId = 999;

        // Act.
        var result = Position.Resolve(invalidId);

        // Assert.
        result.Error.ShouldBe(EnumerationErrors.Unresolved);
    }

    [Fact]
    public void Goalkeeper_ShouldHaveCorrectValue()
    {
        // Assert.
        Position.Goalkeeper.Value.ShouldBe(0);
        Position.Goalkeeper.Name.ShouldBe("Goalkeeper");
    }

    [Fact]
    public void Defender_ShouldHaveCorrectValue()
    {
        // Assert.
        Position.Defender.Value.ShouldBe(1);
        Position.Defender.Name.ShouldBe("Defender");
    }

    [Fact]
    public void Midfielder_ShouldHaveCorrectValue()
    {
        // Assert.
        Position.Midfielder.Value.ShouldBe(2);
        Position.Midfielder.Name.ShouldBe("Midfielder");
    }

    [Fact]
    public void Attacker_ShouldHaveCorrectValue()
    {
        // Assert.
        Position.Attacker.Value.ShouldBe(3);
        Position.Attacker.Name.ShouldBe("Attacker");
    }

    [Fact]
    public void List_ShouldReturnAllEnumerationValues()
    {
        // Assert.
        Position.List.Count.ShouldBe(4);
    }
}