using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.MatchTracking;

public class MatchPeriodTests
{
    [Fact]
    public void Resolve_ShouldReturnPreMatch_WhenIdIsZero()
    {
        // Arrange.
        // Act.
        var result = MatchPeriod.Resolve(0);

        // Assert.
        result.Value.ShouldBe(MatchPeriod.PreMatch);
    }

    [Fact]
    public void Resolve_ShouldReturnFirstHalf_WhenIdIsOne()
    {
        // Arrange.
        // Act.
        var result = MatchPeriod.Resolve(1);

        // Assert.
        result.Value.ShouldBe(MatchPeriod.FirstHalf);
    }

    [Fact]
    public void Resolve_ShouldReturnHalfTime_WhenIdIsThree()
    {
        // Arrange.
        // act.
        var result = MatchPeriod.Resolve(3);

        // Assert.
        result.Value.ShouldBe(MatchPeriod.HalfTime);
    }

    [Fact]
    public void Resolve_ShouldReturnSecondHalf_WhenIdIsFour()
    {
        // Arrange.
        // Act.
        var result = MatchPeriod.Resolve(4);

        // Assert.
        result.Value.ShouldBe(MatchPeriod.SecondHalf);
    }

    [Fact]
    public void Resolve_ShouldReturnFullTime_WhenIdIsSix()
    {
        // Arrange.
        // Act.
        var result = MatchPeriod.Resolve(6);

        // Assert.
        result.Value.ShouldBe(MatchPeriod.FullTime);
    }

    [Fact]
    public void Resolve_ShouldReturnError_WhenIdIsInvalid()
    {
        // Arrange.
        var invalidId = 999;

        // Act.
        var result = MatchPeriod.Resolve(invalidId);

        // Assert.
        result.Error.ShouldBe(EnumerationErrors.Unresolved);
    }

    [Fact]
    public void IsPlayingPeriod_ShouldReturnTrue_WhenFirstHalf()
    {
        // Assert.
        MatchPeriod.FirstHalf.IsPlayingPeriod().ShouldBeTrue();
    }

    [Fact]
    public void IsPlayingPeriod_ShouldReturnTrue_WhenSecondHalf()
    {
        // Assert.
        MatchPeriod.SecondHalf.IsPlayingPeriod().ShouldBeTrue();
    }

    [Fact]
    public void IsPlayingPeriod_ShouldReturnFalse_WhenPreMatch()
    {
        // Assert.
        MatchPeriod.PreMatch.IsPlayingPeriod().ShouldBeFalse();
    }

    [Fact]
    public void IsPlayingPeriod_ShouldReturnFalse_WhenHalfTime()
    {
        // Assert.
        MatchPeriod.HalfTime.IsPlayingPeriod().ShouldBeFalse();
    }

    [Fact]
    public void IsPlayingPeriod_ShouldReturnFalse_WhenFullTime()
    {
        // Assert.
        MatchPeriod.FullTime.IsPlayingPeriod().ShouldBeFalse();
    }

    [Fact]
    public void IsSubstitutionPeriod_ShouldReturnTrue_WhenFirstHalf()
    {
        // Assert.
        MatchPeriod.FirstHalf.IsSubstitutionPeriod().ShouldBeTrue();
    }

    [Fact]
    public void IsSubstitutionPeriod_ShouldReturnTrue_WhenHalfTime()
    {
        // Assert.
        MatchPeriod.HalfTime.IsSubstitutionPeriod().ShouldBeTrue();
    }

    [Fact]
    public void IsSubstitutionPeriod_ShouldReturnTrue_WhenSecondHalf()
    {
        // Assert.
        MatchPeriod.SecondHalf.IsSubstitutionPeriod().ShouldBeTrue();
    }

    [Fact]
    public void IsSubstitutionPeriod_ShouldReturnFalse_WhenPreMatch()
    {
        // Assert.
        MatchPeriod.PreMatch.IsSubstitutionPeriod().ShouldBeFalse();
    }

    [Fact]
    public void IsSubstitutionPeriod_ShouldReturnFalse_WhenFullTime()
    {
        // Assert.
        MatchPeriod.FullTime.IsSubstitutionPeriod().ShouldBeFalse();
    }
}