using Microsoft.EntityFrameworkCore;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;
using SportsStatistics.Tools.DatabaseSeeder.TestData;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface ISeasonSeederService : ISeederService { }

internal sealed class SeasonSeederService(
    ApplicationDbContext dbContext,
    ILogger<SeasonSeederService> logger) : ISeasonSeederService
{
    private const int SeasonCount = 3;

    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<SeasonSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Starting seeding for seasons.");
            
            var dateRanges = BuildSeasonDatesPool();

            var seasons = new SeasonFaker(
                dateRanges)
            .Generate(SeasonCount);

            _dbContext.Seasons.AddRange(seasons);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Finished seeding for seasons.");
            return Result.Success();
        });
    }

    private static Queue<(DateOnly, DateOnly)> BuildSeasonDatesPool()
    {
        var referenceStartDate = new DateTime(DateTime.Today.Year, 8, 1);
        if (referenceStartDate > DateTime.UtcNow)
        {
            referenceStartDate = referenceStartDate.AddYears(-1);
        }
        var referenceEndDate = referenceStartDate.AddYears(1).AddDays(-1);

        var dateRanges = new List<(DateOnly, DateOnly)>();
        for (var i = SeasonCount - 1; i >= 0; i--)
        {
            var startDate = DateOnly.FromDateTime(referenceStartDate).AddYears(-i);
            var endDate = DateOnly.FromDateTime(referenceEndDate).AddYears(-i);

            dateRanges.Add((startDate, endDate));
        }

        return new Queue<(DateOnly, DateOnly)>(dateRanges);
    }
}
