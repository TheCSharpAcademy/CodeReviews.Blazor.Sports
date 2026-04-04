using SportsStatistics.Domain.Teamsheets;

namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

internal static class TeamsheetMapper
{
    public static TeamsheetResponse ToResponse(this Teamsheet teamsheet, List<TeamsheetPlayerResponse> starters, List<TeamsheetPlayerResponse> substitutes)
        => new(teamsheet.Id,
               teamsheet.FixtureId,
               teamsheet.SubmittedAtUtc,
               starters,
               substitutes);
}
