using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed record SeasonDeletedDomainEvent(Guid Id) : IDomainEvent;
