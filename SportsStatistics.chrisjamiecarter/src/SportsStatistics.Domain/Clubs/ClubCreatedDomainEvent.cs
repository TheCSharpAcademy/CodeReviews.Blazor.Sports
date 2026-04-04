using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Clubs;

public sealed record ClubCreatedDomainEvent(Guid Id) : IDomainEvent;
