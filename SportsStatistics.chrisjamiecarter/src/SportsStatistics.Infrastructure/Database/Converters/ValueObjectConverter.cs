using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal abstract class ValueObjectConverter<TModel, TProvider> : ValueConverter<TModel, TProvider>
{
    protected ValueObjectConverter(
        Expression<Func<TModel, TProvider>> toProviderExpression,
        Expression<Func<TProvider, TModel>> fromProviderExpression)
        : base(toProviderExpression, fromProviderExpression)
    {
    }
}
