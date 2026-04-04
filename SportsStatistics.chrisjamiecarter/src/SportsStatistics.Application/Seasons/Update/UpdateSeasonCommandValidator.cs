using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;

namespace SportsStatistics.Application.Seasons.Update;

internal sealed class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty();

        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate)
            .MustAsync(async (command, start, cancellation) =>
            {
                var hasOverlap = await DoesDateOverlapExistingAsync(dbContext, start, command.SeasonId, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'Start Date' overlaps with an existing season.");


        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate)
            .MustAsync(async (command, end, cancellation) =>
            {
                var hasOverlap = await DoesDateOverlapExistingAsync(dbContext, end, command.SeasonId, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'End Date' overlaps with an existing season.");
    }

    private static async Task<bool> DoesDateOverlapExistingAsync(IApplicationDbContext dbContext, DateOnly date, Guid excludeSeasonId, CancellationToken cancellationToken)
    {
        return await dbContext.Seasons.Where(season => season.Id != excludeSeasonId && season.DateRange.StartDate <= date && season.DateRange.EndDate >= date)
                                      .AnyAsync(cancellationToken);
    }
}
