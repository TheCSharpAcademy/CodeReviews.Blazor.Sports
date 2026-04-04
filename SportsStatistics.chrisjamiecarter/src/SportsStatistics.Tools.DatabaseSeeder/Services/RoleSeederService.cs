using Microsoft.AspNetCore.Identity;
using SportsStatistics.Authorization.Constants;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IRoleSeederService : ISeederService { }

internal sealed class RoleSeederService(
    RoleManager<IdentityRole> roleManager,
    ILogger<RoleSeederService> logger)
    : IRoleSeederService
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly ILogger<RoleSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seeding for roles.");

        var roleNames = Roles.GetRoleNames();

        var results = new List<Result>();
        foreach (var roleName in roleNames)
        {
            results.Add(await SeedRoleAsync(roleName, cancellationToken));
        }

        _logger.LogInformation("Finished seeding for roles.");
        return Result.FirstFailureOrSuccess([.. results]);
    }

    private async Task<Result> SeedRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        // As RoleManager does not accept the cancellation token, manually check.
        if (cancellationToken.IsCancellationRequested)
        {
            return Result.Failure(Error.Problem("Role.Seed", "Cancellation Requested"));
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Seeding role: {Role}", roleName);
        }

        if (await _roleManager.RoleExistsAsync(roleName))
        {
            _logger.LogInformation("Role already exists, exiting.");
            return Result.Success();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Failed to create role {Role}: {Errors}", roleName, errors);
            return Result.Failure(Error.Failure("Role.Create", "Failed to create role"));
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Seeded role: {Role}", roleName);
        }

        return Result.Success();
    }
}
