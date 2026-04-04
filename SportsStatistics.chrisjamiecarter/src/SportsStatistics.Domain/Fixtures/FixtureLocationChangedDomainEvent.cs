using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureLocationChangedDomainEvent(Fixture Fixture,
                                                       Location PreviousLocation) : IDomainEvent;
