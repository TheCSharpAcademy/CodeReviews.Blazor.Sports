namespace SportsStatistics.Web.Contracts.Responses;

internal sealed record ValidationFailureResponse(IEnumerable<ValidationResponse> Errors);
