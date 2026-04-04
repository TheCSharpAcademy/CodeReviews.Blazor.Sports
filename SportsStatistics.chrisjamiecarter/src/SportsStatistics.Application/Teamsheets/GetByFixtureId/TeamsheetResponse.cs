namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

public sealed record TeamsheetResponse(Guid Id,
                                       Guid FixtureId,
                                       DateTime SubmittedAtUtc,
                                       List<TeamsheetPlayerResponse> Starters,
                                       List<TeamsheetPlayerResponse> Substitutes);
