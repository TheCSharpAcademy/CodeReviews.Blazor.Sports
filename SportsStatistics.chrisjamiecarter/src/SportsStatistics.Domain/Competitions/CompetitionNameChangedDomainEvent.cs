using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed record CompetitionNameChangedDomainEvent(Competition Competition, string PreviousName) : IDomainEvent;
