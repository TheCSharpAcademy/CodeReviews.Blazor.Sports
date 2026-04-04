using SportsStatistics.Authorization.Constants;

namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public sealed class AdminUserOptions
    : UserOptionsBase
{
    public AdminUserOptions()
        : base("admin", "admin@sportsstatistics.com", Roles.Administrator) { }
}
