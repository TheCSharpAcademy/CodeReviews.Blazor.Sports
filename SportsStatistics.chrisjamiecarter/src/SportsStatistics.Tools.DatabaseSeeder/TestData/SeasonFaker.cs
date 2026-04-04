using Bogus;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.TestData;

internal sealed class SeasonFaker
    : Faker<Season>
{
    public SeasonFaker(
        Queue<(DateOnly, DateOnly)> dateRanges)
    {
        CustomInstantiator(faker =>
        {
            var dateRange = dateRanges.Dequeue();
            return Season.Create(
                Unwrap(DateRange.Create(dateRange.Item1, dateRange.Item2)));
        });
    }

    private static T Unwrap<T>(Result<T> result) =>
        result.IsSuccess
            ? result.Value
            : throw new InvalidOperationException($"Faker produced invalid data: {result.Error}");
}
