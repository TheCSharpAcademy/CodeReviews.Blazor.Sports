namespace SportsStatistics.Web.Contracts.Responses;

internal sealed record SignoutResponse(bool IsSuccess,
                                       string Message);
