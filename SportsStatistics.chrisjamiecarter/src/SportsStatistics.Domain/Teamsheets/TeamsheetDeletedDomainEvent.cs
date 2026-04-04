using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Teamsheets;

public sealed record TeamsheetDeletedDomainEvent(Guid Id) : IDomainEvent;
