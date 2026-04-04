using Microsoft.Extensions.Options;
using SportsStatistics.Authorization.Constants;

namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public sealed class SeederOptionsValidator : IValidateOptions<SeederOptions>
{
    public ValidateOptionsResult Validate(string? name, SeederOptions options)
    {
        var failures = new List<string>();

        if (string.IsNullOrWhiteSpace(options.DefaultPassword))
            failures.Add($"{nameof(options.DefaultPassword)} must not be empty.");

        if (options.YearsOfData is < 1 or > 10)
            failures.Add($"{nameof(options.YearsOfData)} must be between 1 and 10.");

        ValidateUserOptions(nameof(options.Admin), options.Admin, failures);
        ValidateUserOptions(nameof(options.Reports), options.Reports, failures);
        ValidateUserOptions(nameof(options.Tracker), options.Tracker, failures);

        return failures.Count > 0
            ? ValidateOptionsResult.Fail(failures)
            : ValidateOptionsResult.Success;
    }

    private static void ValidateUserOptions(string section, UserOptionsBase options, List<string> failures)
    {
        ArgumentNullException.ThrowIfNull(failures);

        if (string.IsNullOrWhiteSpace(options.Username))
        {
            failures.Add($"{section}.{nameof(options.Username)} must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(options.Email) || !IsValidEmail(options.Email))
        {
            failures.Add($"{section}.{nameof(options.Email)} must be a valid email.");
        }

        if (string.IsNullOrWhiteSpace(options.Role) || !IsValidRole(options.Role))
        {
            failures.Add($"{section}.{nameof(options.Role)} must be a valid role.");
        }
    }

    private static bool IsValidEmail(string email)
    {
        try 
        { 
            return new System.Net.Mail.MailAddress(email).Address == email; 
        }
        catch 
        { 
            return false; 
        }
    }

    private static bool IsValidRole(string role)
    {
        try
        {
            return Roles.GetRoleNames().Contains(role);
        }
        catch
        {
            return false;
        }
    }
}
