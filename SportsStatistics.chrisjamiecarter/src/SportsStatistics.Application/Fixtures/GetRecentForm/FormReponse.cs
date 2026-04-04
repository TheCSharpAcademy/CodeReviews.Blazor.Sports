using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetRecentForm;

public sealed record FormReponse(
    Guid FixtureId,
    Outcome Outcome,
    int HomeGoals,
    int AwayGoals,
    string Opponent);
