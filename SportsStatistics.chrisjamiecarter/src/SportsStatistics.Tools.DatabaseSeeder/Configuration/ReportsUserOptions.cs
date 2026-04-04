using SportsStatistics.Authorization.Constants;

namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public sealed class ReportsUserOptions
    : UserOptionsBase
{
    public ReportsUserOptions()
        : base("reports", "reports@sportsstatistics.com", Roles.ReportsViewer) { }
}
