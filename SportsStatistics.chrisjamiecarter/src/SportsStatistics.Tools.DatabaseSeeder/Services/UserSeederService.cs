using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SportsStatistics.Authorization.Entities;
using SportsStatistics.SharedKernel;
using SportsStatistics.Tools.DatabaseSeeder.Configuration;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IUserSeederService : ISeederService { }

internal sealed class UserSeederService(
    UserManager<ApplicationUser> userManager,
    IOptions<SeederOptions> options,
    ILogger<UserSeederService> logger)
    : IUserSeederService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SeederOptions _options = options.Value;
    private readonly ILogger<UserSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seeding for users.");

        var adminUserResult = await SeedUserAsync(
            _options.Admin.Username,
            _options.Admin.Email,
            _options.Admin.Role,
            cancellationToken);

        var reportsUserResult = await SeedUserAsync(
            _options.Reports.Username,
            _options.Reports.Email,
            _options.Reports.Role,
            cancellationToken);

        var trackerUserResult = await SeedUserAsync(
            _options.Tracker.Username,
            _options.Tracker.Email,
            _options.Tracker.Role,
            cancellationToken);

        _logger.LogInformation("Finished seeding for users.");
        return Result.FirstFailureOrSuccess(
            adminUserResult,
            reportsUserResult,
            trackerUserResult);
    }

    private async Task<Result> SeedUserAsync(string username, string email, string role, CancellationToken cancellationToken)
    {
        // As UserManager does not accept the cancellation token, manually check.
        if (cancellationToken.IsCancellationRequested)
        {
            return Result.Failure(Error.Problem("User.Seed", "Cancellation Requested"));
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Seeding user: {UserName}", username);
        }

        var existingUserEmail = await _userManager.FindByEmailAsync(email);
        var existingUserName = await _userManager.FindByNameAsync(username);
        if (existingUserEmail is not null || existingUserName is not null)
        {
            _logger.LogInformation("User already exists, exiting.");
            return Result.Success();
        }

        var user = new ApplicationUser
        {
            Email = email,
            EmailConfirmed = true,
            UserName = username,
        };

        var createResult = await _userManager.CreateAsync(user, _options.DefaultPassword);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            _logger.LogError("Failed to create user {Username}: {Errors}", username, errors);
            return Result.Failure(Error.Failure("User.Create", "Failed to create user"));
        }

        _logger.LogInformation("Created user");

        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            _logger.LogError("Failed to add user {Username} to role {Role}: {Errors}", username, role, errors);
            return Result.Failure(Error.Failure("User.AddToRole", "Failed to add user to role"));
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Seeded user: {UserName}", username);
        }

        return Result.Success();
    }
}
