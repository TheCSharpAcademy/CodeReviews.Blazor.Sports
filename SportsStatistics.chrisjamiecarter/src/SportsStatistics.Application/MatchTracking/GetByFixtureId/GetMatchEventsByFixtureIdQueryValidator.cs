using FluentValidation;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

internal sealed class GetMatchEventsByFixtureIdQueryValidator : AbstractValidator<GetMatchEventsByFixtureIdQuery>
{
    public GetMatchEventsByFixtureIdQueryValidator()
    {
        RuleFor(q => q.FixtureId)
            .NotEmpty().WithError(MatchEventErrors.FixtureIdIsRequired);
    }
}
