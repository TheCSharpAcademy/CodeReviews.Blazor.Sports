//using SportsStatistics.Domain.MatchTracking;
//using SportsStatistics.Domain.MatchTracking.MatchEvents;
//using SportsStatistics.Domain.Tests.MatchTracking.TestCases;

//namespace SportsStatistics.Domain.Tests.MatchTracking;

//public class MatchEventTests
//{
//    [Theory]
//    [ClassData(typeof(CreateMatchEventTestCase))]
//    public void Create_ShouldCreateMatchEvent_WhenParametersAreValid(Guid fixtureId, MatchEventType matchEventType, Minute minute, DateTime occurredAtUtc)
//    {
//        // Arrange.
//        // Act.
//        var matchEvent = MatchEvent.Create(fixtureId, matchEventType, minute, occurredAtUtc);

//        // Assert.
//        matchEvent.ShouldNotBeNull();
//        matchEvent.Id.ShouldNotBe(default);
//        matchEvent.Id.Version.ShouldBe(7);
//        matchEvent.FixtureId.ShouldBeEquivalentTo(fixtureId);
//        matchEvent.Type.ShouldBeEquivalentTo(matchEventType);
//        matchEvent.Minute.ShouldBeEquivalentTo(minute);
//        matchEvent.OccurredAtUtc.ShouldBeEquivalentTo(occurredAtUtc);
//        matchEvent.Deleted.ShouldBeFalse();
//        matchEvent.DeletedOnUtc.ShouldBeNull();
//    }
//}
