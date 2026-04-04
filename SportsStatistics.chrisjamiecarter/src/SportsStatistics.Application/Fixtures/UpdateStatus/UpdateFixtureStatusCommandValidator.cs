using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.UpdateStatus;

internal sealed class UpdateFixtureStatusCommandValidator : AbstractValidator<UpdateFixtureStatusCommand>
{
    public UpdateFixtureStatusCommandValidator()
    {
        RuleFor(command => command.FixtureId)
            .NotEmpty().WithError(FixtureErrors.Id.IsRequired);

        RuleFor(command => command.FixtureStatus)
            .NotNull().WithError(FixtureErrors.Status.IsRequired);
    }
}
