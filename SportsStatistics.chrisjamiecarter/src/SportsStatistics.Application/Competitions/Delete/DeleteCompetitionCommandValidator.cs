using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Delete;

internal sealed class DeleteCompetitionCommandValidator : AbstractValidator<DeleteCompetitionCommand>
{
    public DeleteCompetitionCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithError(CompetitionErrors.Id.IsRequired);
    }
}
