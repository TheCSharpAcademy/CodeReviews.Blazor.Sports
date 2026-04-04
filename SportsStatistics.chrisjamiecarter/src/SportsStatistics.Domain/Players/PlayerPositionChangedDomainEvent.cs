using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerPositionChangedDomainEvent(Player Player, Position PreviousPosition) : IDomainEvent;
