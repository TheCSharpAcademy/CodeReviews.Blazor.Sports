using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetTopAssistsBySeason;

internal sealed class GetTopAssistsBySeasonQueryValidator
    : AbstractValidator<GetTopAssistsBySeasonQuery>
{
    public GetTopAssistsBySeasonQueryValidator()
    {
        RuleFor(query => query.SeasonId)
            .NotEmpty().WithError(GetTopAssistsBySeasonQueryErrors.SeasonIdIsRequired);

        RuleFor(query => query.Count)
            .GreaterThan(0).WithError(GetTopAssistsBySeasonQueryErrors.CountLessThanOrEqualToZero);
    }
}
