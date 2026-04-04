using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Seasons.GetCurrent;

public sealed record GetCurrentSeasonQuery
    : IQuery<SeasonResponse>;
