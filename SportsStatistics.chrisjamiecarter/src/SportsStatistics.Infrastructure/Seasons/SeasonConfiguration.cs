using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Seasons;

internal sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable(Schemas.Seasons.Table, Schemas.Seasons.Schema);

        builder.HasKey(season => season.Id);

        builder.Property(season => season.Id)
               .HasColumnName(nameof(Season.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.ComplexProperty(season => season.DateRange, complexBuilder =>
        {
            complexBuilder.Property(dateRange => dateRange.StartDate)
                          .HasColumnName(nameof(DateRange.StartDate))
                          .IsRequired();

            complexBuilder.Property(dateRange => dateRange.EndDate)
                          .HasColumnName(nameof(DateRange.EndDate))
                          .IsRequired();
        });

        builder.Ignore(season => season.Name);

        builder.Property(season => season.DeletedOnUtc)
               .HasColumnName(nameof(Season.DeletedOnUtc));

        builder.Property(season => season.Deleted)
               .HasColumnName(nameof(Season.Deleted))
               .HasDefaultValue(false);

        builder.HasQueryFilter(season => !season.Deleted);
    }
}
