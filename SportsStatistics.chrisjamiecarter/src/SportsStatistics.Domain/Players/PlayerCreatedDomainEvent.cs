using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerCreatedDomainEvent(Guid Id) : IDomainEvent;
