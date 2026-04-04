using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureScoreChangedDomainEvent(
    Fixture Fixture,
    Score PreviousScore) : IDomainEvent;
