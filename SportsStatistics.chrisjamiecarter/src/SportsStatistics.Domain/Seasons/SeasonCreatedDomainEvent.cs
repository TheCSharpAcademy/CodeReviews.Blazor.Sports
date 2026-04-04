using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed record SeasonCreatedDomainEvent(Guid Id) : IDomainEvent;
