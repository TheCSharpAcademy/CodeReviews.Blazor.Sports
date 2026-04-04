namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

public sealed record TeamsheetPlayerResponse(
    Guid PlayerId,
    string Name,
    int SquadNumber,
    string Position);
