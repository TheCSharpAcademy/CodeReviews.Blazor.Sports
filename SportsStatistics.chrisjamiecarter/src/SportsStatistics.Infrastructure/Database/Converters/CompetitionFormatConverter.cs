using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class CompetitionFormatConverter : ValueObjectConverter<Format, int>
{
    public CompetitionFormatConverter() : base(type => type.Value, value => Format.Resolve(value).Value) { }
}
