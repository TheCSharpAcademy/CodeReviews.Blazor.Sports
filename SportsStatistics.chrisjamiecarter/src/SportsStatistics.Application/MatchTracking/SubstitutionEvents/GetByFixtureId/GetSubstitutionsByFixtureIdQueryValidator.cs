using FluentValidation;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.GetByFixtureId;

internal sealed class GetSubstitutionsByFixtureIdQueryValidator : AbstractValidator<GetSubstitutionsByFixtureIdQuery>
{
    public GetSubstitutionsByFixtureIdQueryValidator()
    {
        RuleFor(q => q.FixtureId)
            .NotEmpty().WithError(SubstitutionEventErrors.FixtureIdIsRequired);
    }
}