using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

public sealed class GetPlayerFixtureStatisticsQueryValidator
    : AbstractValidator<GetPlayerFixtureStatisticsQuery>
{
    public GetPlayerFixtureStatisticsQueryValidator()
    {
        RuleFor(query => query.FixtureId)
            .NotEmpty().WithError(GetPlayerFixtureStatisticsQueryErrors.FixtureIdIsRequired);
    }
}
