using Microsoft.EntityFrameworkCore;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseMigrator.Services;

internal interface IDatabaseMigratorService
{
    Task<Result> MigrateAsync(DbContext dbContext, CancellationToken cancellationToken = default);
}

internal sealed class DatabaseMigratorService(
    ILogger<DatabaseMigratorService> logger)
    : IDatabaseMigratorService
{
    private readonly ILogger<DatabaseMigratorService> _logger = logger;

    public async Task<Result> MigrateAsync(DbContext dbContext, CancellationToken cancellationToken = default)
    {
        var dbContextName = dbContext.GetType().Name;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Starting migration for {ContextName}.", dbContextName);
        }

        var strategy = dbContext.Database.CreateExecutionStrategy();

        _logger.LogInformation("Checking database connectivity...");
        var canConnect = await strategy.ExecuteAsync(async () =>
        {
            return await dbContext.Database.CanConnectAsync(cancellationToken);
        });

        if (!canConnect)
        {
            return Result.Failure(Error.Failure($"{dbContextName}.CannotConnect", $"Cannot connect to {dbContextName} database."));
        }

        _logger.LogInformation("Checking for pending migrations...");
        var pendingMigrations = await strategy.ExecuteAsync(async () =>
        {
            return await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
        });

        var migrationCount = pendingMigrations.Count();

        if (migrationCount is 0)
        {
            _logger.LogInformation("No pending migrations to apply.");
            return Result.Success();
        }

        _logger.LogInformation("Applying pending migrations...");
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        });

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Completed migration for {ContextName}.", dbContextName);
        }

        return Result.Success();
    }
}
