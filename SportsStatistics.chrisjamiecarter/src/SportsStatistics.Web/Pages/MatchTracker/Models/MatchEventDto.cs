namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed record MatchEventDto(string DisplayText,
                                   DateTime OccuredAtUtc,
                                   Guid? FixtureId = null,
                                   Guid? PlayerId = null,
                                   int? EventTypeId = null,
                                   int Minute = 0,
                                   string EventType = "Display");
