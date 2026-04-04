//using SportsStatistics.Domain.MatchTracking;
//using SportsStatistics.Domain.MatchTracking.PlayerEvents;
//using SportsStatistics.Domain.Tests.MatchTracking.TestCases;

//namespace SportsStatistics.Domain.Tests.MatchTracking;

//public class PlayerEventTests
//{
//    [Theory]
//    [ClassData(typeof(CreatePlayerEventTestCase))]
//    public void Create_ShouldCreatePlayerEvent_WhenParametersAreValid(Guid fixtureId, Guid playerId, PlayerEventType playerEventType, Minute minute, DateTime occurredAtUtc)
//    {
//        // Arrange.
//        // Act.
//        var playerEvent = PlayerEvent.Create(fixtureId, playerId, playerEventType, minute, occurredAtUtc);

//        // Assert.
//        playerEvent.ShouldNotBeNull();
//        playerEvent.Id.ShouldNotBe(default);
//        playerEvent.Id.Version.ShouldBe(7);
//        playerEvent.FixtureId.ShouldBeEquivalentTo(fixtureId);
//        playerEvent.PlayerId.ShouldBeEquivalentTo(playerId);
//        playerEvent.Type.ShouldBeEquivalentTo(playerEventType);
//        playerEvent.Minute.ShouldBeEquivalentTo(minute);
//        playerEvent.OccurredAtUtc.ShouldBeEquivalentTo(occurredAtUtc);
//        playerEvent.Deleted.ShouldBeFalse();
//        playerEvent.DeletedOnUtc.ShouldBeNull();
//    }
//}
