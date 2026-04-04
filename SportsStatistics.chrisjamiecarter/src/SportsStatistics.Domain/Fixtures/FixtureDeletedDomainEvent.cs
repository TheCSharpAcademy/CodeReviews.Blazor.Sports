using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureDeletedDomainEvent(Guid CompetitionId) : IDomainEvent;
