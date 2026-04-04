using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Clubs;

public sealed record ClubNameChangedDomainEvent(Club Club, Name PreviousName) : IDomainEvent;
