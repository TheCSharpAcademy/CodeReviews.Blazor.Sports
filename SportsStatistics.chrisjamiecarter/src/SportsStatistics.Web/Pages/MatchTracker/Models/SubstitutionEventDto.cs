namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed record SubstitutionEventDto(
    Guid FixtureId,
    Guid PlayerOffId,
    Guid PlayerOnId,
    string DisplayText,
    DateTime OccurredAtUtc);
