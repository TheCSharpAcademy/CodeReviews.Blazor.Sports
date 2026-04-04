using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;

namespace SportsStatistics.Application.Seasons.Create;

internal sealed class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
{
    public CreateSeasonCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate)
            .MustAsync(async (start, cancellation) =>
            {
                var hasOverlap = await DoesDateOverlapExistingAsync(dbContext, start, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'Start Date' overlaps with an existing season.");

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate)
            .MustAsync(async (end, cancellation) =>
            {
                var hasOverlap = await DoesDateOverlapExistingAsync(dbContext, end, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'End Date' overlaps with an existing season.");
    }

    private static async Task<bool> DoesDateOverlapExistingAsync(IApplicationDbContext dbContext, DateOnly date, CancellationToken cancellationToken)
    {
        return await dbContext.Seasons.Where(season => season.DateRange.StartDate <= date && season.DateRange.EndDate >= date)
                                      .AnyAsync(cancellationToken);
    }
}

