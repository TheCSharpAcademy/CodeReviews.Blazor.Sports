using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureKickoffTimeUtcChangedDomainEvent(Fixture Fixture,
                                                             KickoffTimeUtc PreviousKickoffTimeUtc) : IDomainEvent
{
}