using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Teamsheets;

public sealed record TeamsheetCreatedDomainEvent(Guid Id, Guid FixtureId) : IDomainEvent;
