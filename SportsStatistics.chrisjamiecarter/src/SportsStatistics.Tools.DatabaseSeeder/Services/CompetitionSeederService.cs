using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;
using SportsStatistics.Tools.DatabaseSeeder.TestData;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface ICompetitionSeederService : ISeederService { }

internal sealed class CompetitionSeederService(
    ApplicationDbContext dbContext,
    ILogger<CompetitionSeederService> logger)
    : ICompetitionSeederService
{
    private const int CompetitionCount = 2;
    private const string LeagueName = "First Division";
    private const string CupName = "Divisional Cup";

    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<CompetitionSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Starting seeding for competitions.");

            var seasons = await _dbContext.Seasons
                .AsNoTracking()
                .OrderBy(season => season.DateRange.StartDate)
                .ToListAsync(cancellationToken);


            foreach (var season in seasons)
            {
                var testData = BuildCompetitionPool();

                var competitions = new CompetitionFaker(
                    season,
                    testData)
                .Generate(CompetitionCount);

                _dbContext.Competitions.AddRange(competitions);
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Finished seeding for competitions.");
            return Result.Success();
        });
    }

    private static Queue<(string, Format)> BuildCompetitionPool()
    {
        var competitions = new List<(string, Format)>
        {
            (LeagueName, Format.League),
            (CupName, Format.Cup)
        };

        return new Queue<(string, Format)>(competitions);
    }
}
