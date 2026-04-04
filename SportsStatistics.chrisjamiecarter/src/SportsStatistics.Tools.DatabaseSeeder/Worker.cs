using System.Diagnostics;
using Microsoft.Extensions.Options;
using SportsStatistics.Tools.DatabaseSeeder.Configuration;
using SportsStatistics.Tools.DatabaseSeeder.Services;

namespace SportsStatistics.Tools.DatabaseSeeder;

public class Worker(
    IHostApplicationLifetime hostApplicationLifetime,
    ILogger<Worker> logger,
    IServiceProvider serviceProvider,
    IOptions<SeederOptions> options)
    : BackgroundService
{
    public const string ActivitySourceName = nameof(DatabaseSeeder);
    
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;
    private readonly ILogger<Worker> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly SeederOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = ActivitySource.StartActivity("Seeding database", ActivityKind.Client);

        try
        {
            using var scope = _serviceProvider.CreateScope();

            // 1. Seed Roles (prerequisite for users).
            var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeederService>();
            await roleSeeder.SeedAsync(stoppingToken);

            // 2. Seed Users (depends on roles).
            var userSeeder = scope.ServiceProvider.GetRequiredService<IUserSeederService>();
            await userSeeder.SeedAsync(stoppingToken);

            // 3. Seed Club (domain data).
            var clubSeeder = scope.ServiceProvider.GetRequiredService<IClubSeederService>();
            await clubSeeder.SeedAsync(stoppingToken);

            // 4. Seed Test Data (optional, configurable).
            if (_options.SeedTestData)
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeederService>();
                await dataSeeder.SeedAsync(stoppingToken);
            }

        }
        catch (Exception exception)
        {
            activity?.AddException(exception);
            throw;
        }

        _hostApplicationLifetime.StopApplication();
    }
}
