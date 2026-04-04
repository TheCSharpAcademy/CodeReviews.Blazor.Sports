namespace SportsStatistics.Web.Contracts.Responses;

internal sealed record ValidationResponse(string PropertyName,
                                          string Message);
