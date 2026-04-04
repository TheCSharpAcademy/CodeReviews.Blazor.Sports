using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Competitions.GetAll;

public sealed record GetAllCompetitionsQuery(Guid SeasonId) : IQuery<List<CompetitionResponse>>;
