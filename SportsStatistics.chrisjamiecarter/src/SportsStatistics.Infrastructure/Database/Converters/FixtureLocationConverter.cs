using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class FixtureLocationConverter : ValueObjectConverter<Location, int>
{
    public FixtureLocationConverter() : base(type => type.Value, value => Location.Resolve(value).Value) { }
}
