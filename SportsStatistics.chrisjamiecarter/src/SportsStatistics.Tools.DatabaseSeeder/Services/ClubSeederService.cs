using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;
using ClubName = SportsStatistics.Domain.Clubs.Name;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IClubSeederService : ISeederService { }

internal sealed class ClubSeederService(
    ApplicationDbContext dbContext,
    ILogger<ClubSeederService> logger)
    : IClubSeederService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<ClubSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seeding for club.");

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            var result = await SeedClubAsync(cancellationToken);

            _logger.LogInformation("Finished seeding for club.");
            return Result.FirstFailureOrSuccess(result);
        });
    }

    private async Task<Result> SeedClubAsync(CancellationToken cancellationToken)
    {
        var nameResult = ClubName.Create(ClubName.DefaultValue);
        if (nameResult.IsFailure)
        {
            _logger.LogError("Failed to create club name: {Error}", nameResult.Error);
            return nameResult;
        }

        var clubName = nameResult.Value;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Seeding club: {Club}", clubName.Value);
        }

        if (await _dbContext.Clubs.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Club already exists, exiting.");
            return Result.Success();
        }

        var club = Club.Create(nameResult.Value);

        _dbContext.Clubs.Add(club);

        await _dbContext.SaveChangesAsync(cancellationToken);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Seeded club: {Club}", clubName.Value);
        }

        return Result.Success();
    }
}
