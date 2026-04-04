using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetTopScorersBySeason;

internal sealed class GetTopScorersBySeasonQueryValidator
    : AbstractValidator<GetTopScorersBySeasonQuery>
{
    public GetTopScorersBySeasonQueryValidator()
    {
        RuleFor(query => query.SeasonId)
            .NotEmpty().WithError(GetTopScorersBySeasonQueryErrors.SeasonIdIsRequired);

        RuleFor(query => query.Count)
            .GreaterThan(0).WithError(GetTopScorersBySeasonQueryErrors.CountLessThanOrEqualToZero);
    }
}
