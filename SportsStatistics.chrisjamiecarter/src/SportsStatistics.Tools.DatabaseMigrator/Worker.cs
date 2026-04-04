using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Authorization.Database;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Tools.DatabaseMigrator.Services;

namespace SportsStatistics.Tools.DatabaseMigrator;

public class Worker(
    IHostApplicationLifetime hostApplicationLifetime,
    IServiceProvider serviceProvider)
    : BackgroundService
{
    public const string ActivitySourceName = nameof(DatabaseMigrator);

    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = _serviceProvider.CreateScope();

            var migratorService = scope.ServiceProvider.GetRequiredService<IDatabaseMigratorService>();

            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var identityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

            foreach (var dbContext in new List<DbContext>() { applicationDbContext, identityDbContext} )
            {
                var result = await migratorService.MigrateAsync(dbContext, stoppingToken);
                
                if (result.IsFailure)
                {
                    throw new InvalidOperationException(result.Error.Description);
                }
            }

            //var seederService = scope.ServiceProvider.GetRequiredService<IDatabaseSeederService>();

            //var seederResult = await seederService.SeedAsync(stoppingToken);

            //if (seederResult.IsSuccess)
            //{
            //    if (_logger.IsEnabled(LogLevel.Information))
            //    {
            //        _logger.LogInformation("Database seeding completed successfully.");
            //    }
            //}
            //else
            //{
            //    throw new InvalidOperationException(seederResult.Error.ToString());
            //}
        }
        catch (Exception exception)
        {
            activity?.AddException(exception);
            throw;
        }

        _hostApplicationLifetime.StopApplication();
    }
}
