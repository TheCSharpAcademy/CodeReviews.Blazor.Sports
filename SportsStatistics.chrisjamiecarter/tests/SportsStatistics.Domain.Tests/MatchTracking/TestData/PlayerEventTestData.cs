//using SportsStatistics.Domain.Fixtures;
//using SportsStatistics.Domain.MatchTracking;
//using SportsStatistics.Domain.MatchTracking.PlayerEvents;
//using SportsStatistics.Domain.Players;
//using SportsStatistics.Domain.Tests.Fixtures.TestData;
//using SportsStatistics.Domain.Tests.Players.TestData;

//namespace SportsStatistics.Domain.Tests.MatchTracking.TestData;

//public static class PlayerEventTestData
//{
//    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

//    private static readonly Player Player = PlayerTestData.ValidPlayer;

//    private static readonly PlayerEventType PlayerEventType = PlayerEventTypeTestData.ValidPlayerEventType;

//    private static readonly Minute Minute = MinuteTestData.ValidMinute;

//    private static readonly DateTime OccurredAtUtc = DateTime.UtcNow;

//    public static PlayerEvent ValidPlayerEvent => PlayerEvent.Create(Fixture.Id, Player.Id, PlayerEventType, Minute, OccurredAtUtc);
//}
