using FluentValidation;

namespace SportsStatistics.Application.Seasons.GetById;

internal sealed class GetSeasonByIdQueryValidator : AbstractValidator<GetSeasonByIdQuery>
{
    public GetSeasonByIdQueryValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty();
    }
}
