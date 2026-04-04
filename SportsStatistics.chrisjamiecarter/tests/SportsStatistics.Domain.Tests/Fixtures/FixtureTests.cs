using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestCases;

namespace SportsStatistics.Domain.Tests.Fixtures;

public class FixtureTests
{
    [Theory]
    [ClassData(typeof(CreateFixtureTestCase))]
    public void Create_ShouldCreateFixture_WhenParametersAreValid(Competition competition, Opponent opponent, KickoffTimeUtc kickoffTimeUtc, Location location)
    {
        // Arrange.
        // Act.
        var fixture = Fixture.Create(competition,
                                     opponent,
                                     kickoffTimeUtc,
                                     location);

        // Assert.
        fixture.ShouldNotBeNull();
        fixture.Id.ShouldNotBe(default);
        fixture.Id.Version.ShouldBe(7);
        fixture.CompetitionId.ShouldBe(competition.Id);
        fixture.Opponent.ShouldBe(opponent);
        fixture.KickoffTimeUtc.ShouldBe(kickoffTimeUtc);
        fixture.Location.ShouldBe(location);
        fixture.Deleted.ShouldBeFalse();
        fixture.DeletedOnUtc.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(FixtureCreatedDomainEventTestCase))]
    public void Create_ShouldRaiseFixtureCreatedDomainEvent_WhenCreatingFixture(Competition competition, Opponent opponent, KickoffTimeUtc kickoffTimeUtc, Location location)
    {
        // Arrange.
        // Act.
        var fixture = Fixture.Create(competition,
                                     opponent,
                                     kickoffTimeUtc,
                                     location);

        fixture.DomainEvents.ShouldHaveSingleItem()
                            .ShouldBeEquivalentTo(new FixtureCreatedDomainEvent(fixture.Id));
    }

    [Theory]
    [ClassData(typeof(ChangeOpponentDifferentTestCase))]
    public void ChangeOpponent_ShouldChangeOpponent_WhenOpponentIsDifferent(Fixture fixture, Opponent opponent)
    {
        // Arrange.
        // Act.
        var result = fixture.ChangeOpponent(opponent);

        // Assert.
        //result.ShouldBeTrue();
        fixture.Opponent.ShouldBeEquivalentTo(opponent);
    }

    [Theory]
    [ClassData(typeof(FixtureOpponentChangedDomainEventTestCase))]
    public void ChangeOpponent_ShouldRaiseFixtureOpponentChangedDomainEvent_WhenOpponentIsChanged(Fixture fixture, Opponent opponent, FixtureOpponentChangedDomainEvent expected)
    {
        // Arrange.
        fixture.ClearDomainEvents();

        // Act.
        fixture.ChangeOpponent(opponent);

        // Assert.
        fixture.DomainEvents.ShouldHaveSingleItem()
                            .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeOpponentIdenticalTestCase))]
    public void ChangeOpponent_ShouldNotChangeOpponent_WhenOpponentIsIdentical(Fixture fixture, Opponent opponent)
    {
        // Arrange.
        // Act.
        var result = fixture.ChangeOpponent(opponent);

        // Assert.
        //result.ShouldBeFalse();
        fixture.Opponent.ShouldBeEquivalentTo(opponent);
    }

    [Theory]
    [ClassData(typeof(ChangeKickoffTimeUtcDifferentTestCase))]
    public void ChangeKickoffTimeUtc_ShouldChangeKickoffTimeUtc_WhenKickoffTimeUtcIsDifferent(Fixture fixture, KickoffTimeUtc kickoffTimeUtc)
    {
        // Arrange.
        // Act.
        var result = fixture.ChangeKickoffTimeUtc(kickoffTimeUtc);

        // Assert.
        //result.ShouldBeTrue();
        fixture.KickoffTimeUtc.ShouldBeEquivalentTo(kickoffTimeUtc);
    }

    [Theory]
    [ClassData(typeof(FixtureKickoffTimeUtcChangedDomainEventTestCase))]
    public void ChangeKickoffTimeUtc_ShouldRaiseFixtureKickoffTimeUtcChangedDomainEvent_WhenKickoffTimeUtcIsChanged(Fixture fixture, KickoffTimeUtc kickoffTimeUtc, FixtureKickoffTimeUtcChangedDomainEvent expected)
    {
        // Arrange.
        fixture.ClearDomainEvents();

        // Act.
        fixture.ChangeKickoffTimeUtc(kickoffTimeUtc);

        // Assert.
        fixture.DomainEvents.ShouldHaveSingleItem()
                            .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeKickoffTimeUtcIdenticalTestCase))]
    public void ChangeKickoffTimeUtc_ShouldNotChangeKickoffTimeUtc_WhenKickoffTimeUtcIsIdentical(Fixture fixture, KickoffTimeUtc kickoffTimeUtc)
    {
        // Arrange.
        // Act.
        var result = fixture.ChangeKickoffTimeUtc(kickoffTimeUtc);

        // Assert.
        //result.ShouldBeFalse();
        fixture.KickoffTimeUtc.ShouldBeEquivalentTo(kickoffTimeUtc);
    }

    [Theory]
    [ClassData(typeof(ChangeLocationDifferentTestCase))]
    public void ChangeLocation_ShouldChangeLocation_WhenLocationIsDifferent(Fixture competition, Location location)
    {
        // Arrange.
        // Act.
        var result = competition.ChangeLocation(location);

        // Assert.
        //result.ShouldBeTrue();
        competition.Location.ShouldBeEquivalentTo(location);
    }

    [Theory]
    [ClassData(typeof(FixtureLocationChangedDomainEventTestCase))]
    public void ChangeLocation_ShouldRaiseFixtureLocationChangedDomainEvent_WhenLocationIsChanged(Fixture fixture, Location location, FixtureLocationChangedDomainEvent expected)
    {
        // Arrange.
        fixture.ClearDomainEvents();

        // Act.
        fixture.ChangeLocation(location);

        // Assert.
        fixture.DomainEvents.ShouldHaveSingleItem()
                            .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeLocationIdenticalTestCase))]
    public void ChangeLocation_ShouldNotChangeLocation_WhenLocationIsIdentical(Fixture fixture, Location location)
    {
        // Arrange.
        // Act.
        var result = fixture.ChangeLocation(location);

        // Assert.
        //result.ShouldBeFalse();
        fixture.Location.ShouldBeEquivalentTo(location);
    }

    [Theory]
    [ClassData(typeof(DeleteFixtureTestCase))]
    public void Delete_ShouldDeleteFixture_WhenFixtureIsNotAlreadyDeleted(Fixture fixture, DateTime utcNow)
    {
        // Arrange.
        // Act.
        fixture.Delete(utcNow);

        // Assert.
        fixture.Deleted.ShouldBeTrue();
        fixture.DeletedOnUtc.ShouldBeEquivalentTo(utcNow);
    }

    [Theory]
    [ClassData(typeof(FixtureDeletedDomainEventTestCase))]
    public void Delete_ShouldRaiseFixtureDeletedDomainEvent_WhenFixtureIsDeleted(Fixture fixture, DateTime utcNow, FixtureDeletedDomainEvent expected)
    {
        // Arrange.
        fixture.ClearDomainEvents();

        // Act.
        fixture.Delete(utcNow);

        // Assert.
        fixture.DomainEvents.ShouldHaveSingleItem()
                            .ShouldBeEquivalentTo(expected);
    }
}
