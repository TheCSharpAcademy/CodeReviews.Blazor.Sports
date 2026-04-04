using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerSquadNumberChangedDomainEvent(Player Player, SquadNumber PreviousSquadNumber) : IDomainEvent;
