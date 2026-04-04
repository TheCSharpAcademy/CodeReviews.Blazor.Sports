using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

internal sealed class GetPlayerSeasonStatisticsQueryValidator
    : AbstractValidator<GetPlayerSeasonStatisticsQuery>
{
    public GetPlayerSeasonStatisticsQueryValidator()
    {
        RuleFor(query => query.SeasonId)
            .NotEmpty().WithError(GetPlayerSeasonStatisticsQueryErrors.SeasonIdIsRequired);
    }
}
