using Bogus;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;
using SportsStatistics.Tools.DatabaseSeeder.TestData;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IPlayerSeederService : ISeederService { }

internal sealed class PlayerSeederService(
    ApplicationDbContext dbContext,
    ILogger<PlayerSeederService> logger)
    : IPlayerSeederService
{
    private const int PlayerCount = 21;

    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<PlayerSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seeding for players.");

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            var faker = new Faker();
            var squadNumbers = BuildSquadNumberPool(faker);
            var positions = BuildPositionPool(faker, 3, 6, 6, 6);

            var players = new PlayerFaker(
                squadNumbers,
                positions)
            .Generate(PlayerCount);

            _dbContext.Players.AddRange(players);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Finished seeding for players.");
            return Result.Success();
        });
    }

    public static Queue<Position> BuildPositionPool(Faker faker, int goalkeepers, int defenders, int midfielders, int attackers)
    {
        var total = goalkeepers + defenders + midfielders + attackers;
        if (total != PlayerCount)
        {
            throw new ArgumentException($"Position counts must sum to {PlayerCount} but got {total}.");
        }

        var positions = new List<Position>();
        positions.AddRange(Enumerable.Repeat(Position.Goalkeeper, goalkeepers));
        positions.AddRange(Enumerable.Repeat(Position.Defender, defenders));
        positions.AddRange(Enumerable.Repeat(Position.Midfielder, midfielders));
        positions.AddRange(Enumerable.Repeat(Position.Attacker, attackers));

        return new Queue<Position>(faker.Random.Shuffle(positions));
    }

    public static Queue<int> BuildSquadNumberPool(Faker faker)
    {
        var range = PlayerCount > 99 ? PlayerCount + 10 : 99;

        var shuffled = faker.Random
            .Shuffle(Enumerable.Range(1, range))
            .Take(PlayerCount);

        return new Queue<int>(shuffled);
    }
}
