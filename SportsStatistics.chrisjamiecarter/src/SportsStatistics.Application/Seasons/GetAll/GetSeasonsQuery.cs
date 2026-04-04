using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.GetAll;

public sealed record GetSeasonsQuery() : IQuery<List<SeasonResponse>>;
