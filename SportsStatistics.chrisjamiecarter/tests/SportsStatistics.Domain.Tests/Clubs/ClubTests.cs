using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestCases;

namespace SportsStatistics.Domain.Tests.Clubs;

public class ClubTests
{
    [Theory]
    [ClassData(typeof(CreateClubTestCase))]
    public void Create_ShouldCreateClub_WhenParametersAreValid(Name name)
    {
        // Arrange.
        // Act.
        var club = Club.Create(name);

        // Assert.
        club.ShouldNotBeNull();
        club.Id.ShouldNotBe(default);
        club.Id.Version.ShouldBe(7);
        club.Name.ShouldBe(name);
    }

    [Theory]
    [ClassData(typeof(ClubCreatedDomainEventTestCase))]
    public void Create_ShouldRaiseClubCreatedDomainEvent_WhenCreatingClub(Name name)
    {
        // Arrange.
        // Act.
        var club = Club.Create(name);

        // Assert.
        club.DomainEvents
            .ShouldHaveSingleItem()
            .ShouldBeOfType<ClubCreatedDomainEvent>();
    }

    [Theory]
    [ClassData(typeof(ChangeNameDifferentTestCase))]
    public void ChangeName_ShouldChangeName_WhenNameIsDifferent(Club club, Name name)
    {
        // Arrange.
        // Act.
        var result = club.ChangeName(name);

        // Assert.
        result.ShouldBeTrue();
        club.Name.ShouldBeEquivalentTo(name);
    }

    [Theory]
    [ClassData(typeof(ClubNameChangedDomainEventTestCase))]
    public void ChangeName_ShouldRaiseClubNameChangedDomainEvent_WhenNameIsChanged(Club club, Name name, ClubNameChangedDomainEvent expected)
    {
        // Arrange.
        club.ClearDomainEvents();

        // Act.
        club.ChangeName(name);

        // Assert.
        club.DomainEvents
            .ShouldHaveSingleItem()
            .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeNameIdenticalTestCase))]
    public void ChangeName_ShouldNotChangeName_WhenNameIsIdentical(Club club, Name name)
    {
        // Arrange.
        // Act.
        var result = club.ChangeName(name);

        // Assert.
        result.ShouldBeFalse();
        club.Name.ShouldBeEquivalentTo(name);
    }

}
