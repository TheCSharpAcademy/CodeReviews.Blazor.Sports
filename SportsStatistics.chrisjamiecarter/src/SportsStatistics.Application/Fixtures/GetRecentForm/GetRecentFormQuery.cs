using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.GetRecentForm;

public sealed record GetRecentFormQuery(
    int Count = 5)
    : IQuery<List<FormReponse>>;
