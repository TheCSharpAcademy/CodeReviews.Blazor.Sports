using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed record DateRange
{
    private DateRange(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; }
    
    public DateOnly EndDate { get; }

    public static Result<DateRange> Create(DateOnly? startDate, DateOnly? endDate)
    {
        if (startDate is null || startDate == DateOnly.MinValue)
        {
            return SeasonErrors.DateRange.StartDate.NullOrEmpty;
        }

        if (endDate is null || endDate == DateOnly.MinValue)
        {
            return SeasonErrors.DateRange.EndDate.NullOrEmpty;
        }

        if (endDate <= startDate)
        {
            return SeasonErrors.DateRange.StartDateAfterEndDate;
        }

        return new DateRange(startDate.Value, endDate.Value);
    }
}
