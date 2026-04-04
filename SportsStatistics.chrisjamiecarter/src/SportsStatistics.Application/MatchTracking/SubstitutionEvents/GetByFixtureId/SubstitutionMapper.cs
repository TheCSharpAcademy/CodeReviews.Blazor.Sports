using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.GetByFixtureId;

internal static class SubstitutionMapper
{
    public static List<SubstitutionResponse> ToResponse(this IEnumerable<SubstitutionEvent> substitutionEvents)
        => [.. substitutionEvents.Select(ToResponse)];

    public static SubstitutionResponse ToResponse(this SubstitutionEvent substitutionEvent)
        => new(substitutionEvent.Id,
               substitutionEvent.Substitution.PlayerOffId,
               substitutionEvent.Substitution.PlayerOnId,
               substitutionEvent.Minute.BaseMinute,
               substitutionEvent.Minute.StoppageMinute,
               substitutionEvent.Minute.Display,
               substitutionEvent.OccurredAtUtc);
}
