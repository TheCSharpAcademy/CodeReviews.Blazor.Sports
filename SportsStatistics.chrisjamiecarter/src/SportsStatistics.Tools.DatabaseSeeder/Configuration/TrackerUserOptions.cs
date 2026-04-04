using SportsStatistics.Authorization.Constants;

namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public sealed class TrackerUserOptions
    : UserOptionsBase
{
    public TrackerUserOptions()
        : base("tracker", "tracker@sportsstatistics.com", Roles.MatchTracker) { }
}
