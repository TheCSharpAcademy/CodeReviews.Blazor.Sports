namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed record FixtureDto(Guid Id,
                                Guid CompetitionId,
                                string CompetitionName,
                                string Opponent,
                                DateTime KickoffTimeUtc,
                                int LocationId,
                                string Location,
                                int HomeGoals,
                                int AwayGoals,
                                int StatusId,
                                string Status)
{
    public string DisplayKickoffDate => $"{KickoffTimeUtc.ToLocalTime():ddd d MMM, HH:mm}";
    public string DisplayKickoffTime => $"{KickoffTimeUtc.ToLocalTime():HH:mm}";
}
