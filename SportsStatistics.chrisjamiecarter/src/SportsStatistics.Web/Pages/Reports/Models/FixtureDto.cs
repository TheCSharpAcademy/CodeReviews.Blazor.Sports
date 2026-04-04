using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Web.Pages.Reports.Models;

public sealed record FixtureDto(
    Guid FixtureId,
    Guid CompetitionId,
    string CompetitionName,
    string ClubName,
    string Opponent,
    DateTime KickoffTimeUtc,
    Location Location,
    Score Score,
    Status Status,
    Outcome Outcome)
{
    public string Title => $"{HomeTeam} {Score.HomeGoals} - {Score.AwayGoals} {AwayTeam} ({CompetitionName})";

    private string HomeTeam => Location == Location.Away ? Opponent : ClubName;

    private string AwayTeam => Location == Location.Away ? ClubName : Opponent;
}
