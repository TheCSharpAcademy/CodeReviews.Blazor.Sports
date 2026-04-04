namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed record DisplayMatchEventDto(
    string MinuteDisplay,
    string EventTypeDisplay,
    DateTime OccurredAtUtc);
