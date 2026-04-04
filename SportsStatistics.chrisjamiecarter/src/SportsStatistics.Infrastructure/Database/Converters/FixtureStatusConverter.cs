using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class FixtureStatusConverter : ValueObjectConverter<Status, int>
{
    public FixtureStatusConverter() : base(type => type.Value, value => Status.Resolve(value).Value) { }
}
