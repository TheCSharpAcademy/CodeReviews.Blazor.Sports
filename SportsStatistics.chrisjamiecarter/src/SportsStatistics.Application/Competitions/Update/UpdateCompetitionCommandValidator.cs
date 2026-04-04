using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandValidator : AbstractValidator<UpdateCompetitionCommand>
{
    public UpdateCompetitionCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithError(CompetitionErrors.Id.IsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(CompetitionErrors.Name.IsRequired)
            .MaximumLength(Name.MaxLength).WithError(CompetitionErrors.Name.ExceedsMaxLength);

        RuleFor(c => c.FormatId)
            .Must(Format.ContainsValue).WithError(CompetitionErrors.Format.NotFound);
    }
}
