using SportsStatistics.Domain.MatchTracking.PlayerEvents;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class PlayerEventTypeConverter : ValueObjectConverter<PlayerEventType, int>
{
    public PlayerEventTypeConverter() : base(type => type.Value, value => PlayerEventType.Resolve(value).Value) { }
}
