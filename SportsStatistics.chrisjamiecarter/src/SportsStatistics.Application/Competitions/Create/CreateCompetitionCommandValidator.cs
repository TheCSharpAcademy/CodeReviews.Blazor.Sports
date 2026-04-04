using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Create;

internal sealed class CreateCompetitionCommandValidator : AbstractValidator<CreateCompetitionCommand>
{
    public CreateCompetitionCommandValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty().WithError(CompetitionErrors.SeasonId.IsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(CompetitionErrors.Name.IsRequired)
            .MaximumLength(Name.MaxLength).WithError(CompetitionErrors.Name.ExceedsMaxLength);

        RuleFor(c => c.FormatId)
            .Must(Format.ContainsValue).WithError(CompetitionErrors.Format.NotFound);
    }
}
