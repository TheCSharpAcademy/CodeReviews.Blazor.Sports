using Bogus;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;
using SportsStatistics.Tools.DatabaseSeeder.TestData;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IFixtureSeederService : ISeederService { }

internal sealed class FixtureSeederService(
    ApplicationDbContext dbContext,
    ILogger<FixtureSeederService> logger)
    : IFixtureSeederService
{
    private static readonly IReadOnlyList<string> OppositionTeams =
    [
        "Riverside Rangers",
        "Oakwood United",
        "Steel City FC",
        "Harborview Athletic",
        "Parkland Wanderers",
        "Meadowbrook City",
        "Ironbridge Town",
        "Crestview Albion",
        "Bayshore United",
        "Valley Rovers"
    ];

    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<FixtureSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Starting seeding for fixtures.");

            var faker = new Faker();

            var seasons = await _dbContext.Seasons
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var season in seasons)
            {
                // NOTE: Assuming seeding one cup and one league competition.
                var cupFixtureData = BuildCupFixtures(faker);
                var leagueFixtureData = BuildLeagueFixtures(faker);
                var matchDates = BuildUniqueMatchDates(
                    faker,
                    season.DateRange.StartDate,
                    season.DateRange.EndDate,
                    cupFixtureData.Count + leagueFixtureData.Count);

                var cupCompetition = await _dbContext.Competitions
                    .AsNoTracking()
                    .Where(competition => competition.SeasonId == season.Id && competition.Format == Format.Cup)
                    .FirstOrDefaultAsync(cancellationToken);

                if (cupCompetition is not null)
                {
                    var cupFixtures = new FixtureFaker(
                        cupCompetition,
                        cupFixtureData,
                        matchDates)
                    .Generate(cupFixtureData.Count);

                    _dbContext.Fixtures.AddRange(cupFixtures);
                }

                var leagueCompetition = await _dbContext.Competitions
                    .AsNoTracking()
                    .Where(competition => competition.SeasonId == season.Id && competition.Format == Format.League)
                    .FirstOrDefaultAsync(cancellationToken);

                if (leagueCompetition is not null)
                {
                    var leagueFixtures = new FixtureFaker(
                        leagueCompetition,
                        leagueFixtureData,
                        matchDates)
                    .Generate(leagueFixtureData.Count);

                    _dbContext.Fixtures.AddRange(leagueFixtures);
                }
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Finished seeding for fixtures.");
            return Result.Success();
        });
    }

    private static Queue<(string, Location)> BuildCupFixtures(Faker faker)
    {
        var cupTeam = faker.Random.Shuffle(OppositionTeams.ToList()).First();

        var fixtures = new List<(string, Location)>
        {
            (cupTeam, Location.Home),
            (cupTeam, Location.Away)
        };

        return new Queue<(string, Location)>(faker.Random.Shuffle(fixtures));
    }

    private static Queue<(string, Location)> BuildLeagueFixtures(Faker faker)
    {
        var fixtures = new List<(string, Location)>();
        fixtures.AddRange(OppositionTeams.Select(x => (x, Location.Home)));
        fixtures.AddRange(OppositionTeams.Select(x => (x, Location.Away)));

        return new Queue<(string, Location)>(faker.Random.Shuffle(fixtures));
    }

    private static Queue<DateTime> BuildUniqueMatchDates(Faker faker, DateOnly start, DateOnly end, int count)
    {
        var totalDays = end.DayNumber - start.DayNumber + 1;

        if (count > totalDays)
            throw new InvalidOperationException(
                $"Cannot generate {count} unique match dates within a {totalDays}-day window.");

        var dates = faker.Random
            .Shuffle(Enumerable.Range(0, totalDays).Select(offset => start.AddDays(offset)))
            .Take(count)
            .Select(d => d.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
                ? new DateTime(d, new TimeOnly(15, 0))
                : new DateTime(d, new TimeOnly(20, 0)));

        return new Queue<DateTime>(dates);
    }
}
