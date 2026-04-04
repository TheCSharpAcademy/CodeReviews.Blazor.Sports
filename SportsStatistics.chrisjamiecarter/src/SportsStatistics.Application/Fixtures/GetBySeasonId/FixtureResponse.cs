namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

public sealed record FixtureResponse(Guid Id,
                                     Guid CompetitionId,
                                     string CompetitionName,
                                     string Opponent,
                                     DateTime KickoffTimeUtc,
                                     int LocationId,
                                     string Location,
                                     int HomeGoals,
                                     int AwayGoals,
                                     int StatusId,
                                     string Status);
