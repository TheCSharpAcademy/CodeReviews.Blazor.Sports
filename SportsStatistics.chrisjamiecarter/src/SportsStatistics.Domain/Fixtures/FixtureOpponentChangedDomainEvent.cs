using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record FixtureOpponentChangedDomainEvent(Fixture Fixture,
                                                       Opponent PreviousOpponent) : IDomainEvent;
