namespace SportsStatistics.Web.Pages.Home.Models;

public sealed record FixtureCardDto(
    Guid FixtureId,
    string CompetitionName,
    string Opponent,
    string Location,
    DateTime KickoffTimeLocal,
    int HomeGoals,
    int AwayGoals,
    string OutcomeOrStatus);
