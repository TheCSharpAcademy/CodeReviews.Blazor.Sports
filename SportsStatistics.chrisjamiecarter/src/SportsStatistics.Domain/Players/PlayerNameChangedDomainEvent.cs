using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerNameChangedDomainEvent(Player Player, Name PreviousName) : IDomainEvent;
