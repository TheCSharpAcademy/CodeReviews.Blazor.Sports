namespace SportsStatistics.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
