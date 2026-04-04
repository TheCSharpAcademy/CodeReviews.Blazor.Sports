using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerNationalityChangedDomainEvent(Player Player, Nationality PreviousNationality) : IDomainEvent;
