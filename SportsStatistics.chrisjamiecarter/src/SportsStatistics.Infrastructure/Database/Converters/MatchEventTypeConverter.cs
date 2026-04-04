using SportsStatistics.Domain.MatchTracking.MatchEvents;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class MatchEventTypeConverter : ValueObjectConverter<MatchEventType, int>
{
    public MatchEventTypeConverter() : base(type => type.Value, value => MatchEventType.Resolve(value).Value) { }
}
