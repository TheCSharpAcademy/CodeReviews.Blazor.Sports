using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerLeftClubDomainEvent(Player Player, DateTime LeftClubOnUtc) : IDomainEvent;
