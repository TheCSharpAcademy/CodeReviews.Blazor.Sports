using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed record CompetitionDeletedDomainEvent(Guid CompetitionId) : IDomainEvent;
