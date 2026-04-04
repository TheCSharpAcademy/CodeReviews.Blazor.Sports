using SportsStatistics.Domain.Players;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class PlayerPositionConverter : ValueObjectConverter<Position, int>
{
    public PlayerPositionConverter() : base(type => type.Value, value => Position.Resolve(value).Value) { }
}
