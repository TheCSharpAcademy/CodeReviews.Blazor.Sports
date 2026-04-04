using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed record CompetitionFormatChangedDomainEvent(Competition Competition, Format PreviousFormat) : IDomainEvent;
