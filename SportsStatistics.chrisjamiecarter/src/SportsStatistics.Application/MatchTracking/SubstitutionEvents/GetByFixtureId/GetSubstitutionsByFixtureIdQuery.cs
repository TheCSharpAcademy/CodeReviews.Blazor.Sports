using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.GetByFixtureId;

public sealed record GetSubstitutionsByFixtureIdQuery(Guid FixtureId) : IQuery<List<SubstitutionResponse>>;
