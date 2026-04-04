using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetSeasonStatistics;

internal sealed class GetSeasonStatisticsQueryValidator
    : AbstractValidator<GetSeasonStatisticsQuery>
{
    public GetSeasonStatisticsQueryValidator()
    {
        RuleFor(query => query.SeasonId)
            .NotEmpty().WithError(GetSeasonStatisticsQueryErrors.SeasonIdIsRequired);
    }
}
