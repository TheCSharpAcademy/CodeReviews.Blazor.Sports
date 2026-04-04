namespace SportsStatistics.Web.Contracts.Requests;

internal sealed record SigninRequest(
    string? Email = null,
    string? Password = null,
    bool IsPersistant = false);
