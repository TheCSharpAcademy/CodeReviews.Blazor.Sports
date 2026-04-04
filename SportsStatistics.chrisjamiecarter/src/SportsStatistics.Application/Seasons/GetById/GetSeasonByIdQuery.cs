using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.GetById;

public sealed record GetSeasonByIdQuery(Guid SeasonId) : IQuery<SeasonResponse>;
