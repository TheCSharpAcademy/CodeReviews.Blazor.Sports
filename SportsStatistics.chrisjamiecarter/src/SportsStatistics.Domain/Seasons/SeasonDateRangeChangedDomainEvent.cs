using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed record SeasonDateRangeChangedDomainEvent(Season Season, DateRange PreviousDateRange) : IDomainEvent;
