using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed record CompetitionCreatedDomainEvent(Guid CompetitionId) : IDomainEvent;
