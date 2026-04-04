using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

public sealed record GetMatchEventsByFixtureIdQuery(Guid FixtureId) : IQuery<List<MatchEventResponse>>;
