using Microsoft.Extensions.Time.Testing;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class PlayerLeftClubDomainEventTestCase : TheoryData<Player, FakeTimeProvider, PlayerLeftClubDomainEvent>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    public PlayerLeftClubDomainEventTestCase()
    {
        var timeProvider = new FakeTimeProvider(new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero));
        Add(Player, timeProvider, new(Player, timeProvider.GetUtcNow().UtcDateTime));
    }
}
