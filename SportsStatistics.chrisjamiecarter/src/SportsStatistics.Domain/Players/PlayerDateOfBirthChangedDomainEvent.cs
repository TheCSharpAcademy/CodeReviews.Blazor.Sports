using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerDateOfBirthChangedDomainEvent(Player Player, DateOfBirth PreviousDateOfBirth) : IDomainEvent;
