namespace SportsStatistics.Web.Pages.Admin.Fixtures.Models;

public sealed record FixtureDto(
    Guid Id,
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
    public string DisplayStatus => Status switch
    {
        "Scheduled" => $"{KickoffTimeUtc.ToLocalTime():HH:mm}",
        _ => $"{HomeGoals} - {AwayGoals}"
    };

    public DateOnly Date => DateOnly.FromDateTime(KickoffTimeUtc.ToLocalTime());
}
