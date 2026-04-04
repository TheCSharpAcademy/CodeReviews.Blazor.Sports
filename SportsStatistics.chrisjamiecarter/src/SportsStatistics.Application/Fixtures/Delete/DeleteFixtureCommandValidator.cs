using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Delete;

internal sealed class DeleteFixtureCommandValidator : AbstractValidator<DeleteFixtureCommand>
{
    public DeleteFixtureCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(FixtureErrors.Id.IsRequired);
    }
}
