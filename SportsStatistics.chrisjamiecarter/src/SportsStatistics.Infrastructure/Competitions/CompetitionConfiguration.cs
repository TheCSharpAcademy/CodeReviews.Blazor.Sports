using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Competitions;

internal sealed class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.ToTable(Schemas.Competitions.Table, Schemas.Competitions.Schema);

        builder.HasKey(competition => competition.Id);

        builder.Property(competition => competition.Id)
               .HasColumnName(nameof(Competition.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(competition => competition.SeasonId)
               .HasColumnName(nameof(Competition.SeasonId))
               .IsRequired();

        builder.ComplexProperty(competition => competition.Name, complexBuilder =>
        {
            complexBuilder.Property(name => name.Value)
                          .HasColumnName(nameof(Competition.Name))
                          .HasMaxLength(Name.MaxLength)
                          .IsRequired();
        });

        builder.Property(competition => competition.Format)
               .HasColumnName(nameof(Competition.Format))
               .IsRequired();

        builder.Property(competition => competition.DeletedOnUtc)
               .HasColumnName(nameof(Competition.DeletedOnUtc));

        builder.Property(competition => competition.Deleted)
               .HasColumnName(nameof(Competition.Deleted))
               .HasDefaultValue(false);

        builder.HasOne<Season>()
               .WithMany()
               .HasForeignKey(competition => competition.SeasonId);

        builder.HasQueryFilter(competition => !competition.Deleted);
    }
}
