using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureCreatedDomainEvent(Guid Id) : IDomainEvent;
