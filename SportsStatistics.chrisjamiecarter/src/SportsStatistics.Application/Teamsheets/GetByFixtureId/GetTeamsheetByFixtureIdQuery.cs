using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

public sealed record GetTeamsheetByFixtureIdQuery(Guid FixtureId) : IQuery<TeamsheetResponse>;
