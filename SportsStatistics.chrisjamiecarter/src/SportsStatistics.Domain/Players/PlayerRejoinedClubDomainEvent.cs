using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerRejoinedClubDomainEvent(Player Player, DateTime? PreviousLeftClubOnUtc) : IDomainEvent;
