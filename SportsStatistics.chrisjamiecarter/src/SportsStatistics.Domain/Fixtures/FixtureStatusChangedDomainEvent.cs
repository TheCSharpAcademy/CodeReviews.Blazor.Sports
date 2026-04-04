using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureStatusChangedDomainEvent(
    Fixture Fixture,
    Status PreviousStatus) : IDomainEvent;
