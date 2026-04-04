using Microsoft.Extensions.Time.Testing;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class PlayerRejoinedClubDomainEventTestCase : TheoryData<Player, FakeTimeProvider, PlayerRejoinedClubDomainEvent>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    public PlayerRejoinedClubDomainEventTestCase()
    {
        var timeProvider = new FakeTimeProvider(new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero));
        Add(Player, timeProvider, new(Player, timeProvider.GetUtcNow().UtcDateTime));
    }
}
