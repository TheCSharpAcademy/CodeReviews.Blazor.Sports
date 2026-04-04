using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Seasons;

public class SeasonBuilder : IBuildable<Season>
{
    private DateOnly _startDate = DateOnly.FromDateTime(DateTime.UtcNow);
    private DateOnly _endDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(1);

    public SeasonBuilder WithStartDate(DateOnly startDate)
    {
        _startDate = startDate;
        return this;
    }

    public SeasonBuilder WithEndDate(DateOnly endDate)
    {
        _endDate = endDate;
        return this;
    }

    private static Season Create(DateOnly startDate, DateOnly endDate) =>
        Season.Create(
            DateRange.Create(startDate, endDate).Value);

    public Season Build() =>
        Create(_startDate, _endDate);

    public static List<Season> GetDefaults()
    {
        var builder = new SeasonBuilder();

        return
        [
            builder.WithStartDate(new(2023, 8, 1)).WithEndDate(new(2024, 7, 31)).Build(),
            builder.WithStartDate(new(2024, 8, 1)).WithEndDate(new(2025, 7, 31)).Build(),
        ];
    }
}
