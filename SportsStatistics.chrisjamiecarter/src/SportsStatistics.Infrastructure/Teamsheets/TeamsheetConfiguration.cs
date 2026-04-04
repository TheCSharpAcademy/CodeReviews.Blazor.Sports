using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Teamsheets;

internal sealed class TeamsheetConfiguration : IEntityTypeConfiguration<Teamsheet>
{
    public void Configure(EntityTypeBuilder<Teamsheet> builder)
    {
        builder.ToTable(Schemas.Teamsheets.Table, Schemas.Teamsheets.Schema);

        builder.HasKey(teamsheet => teamsheet.Id);

        builder.Property(teamsheet => teamsheet.Id)
               .HasColumnName(nameof(Teamsheet.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(teamsheet => teamsheet.FixtureId)
               .HasColumnName(nameof(Teamsheet.FixtureId))
               .IsRequired();

        builder.Property(teamsheet => teamsheet.SubmittedAtUtc)
               .HasColumnName(nameof(Teamsheet.SubmittedAtUtc))
               .IsRequired();

        builder.Property(teamsheet => teamsheet.DeletedOnUtc)
               .HasColumnName(nameof(Teamsheet.DeletedOnUtc));

        builder.Property(teamsheet => teamsheet.Deleted)
               .HasColumnName(nameof(Teamsheet.Deleted))
               .HasDefaultValue(false);

        builder.HasMany(teamsheet => teamsheet.Players)
               .WithOne()
               .HasForeignKey(teamsheetPlayer => teamsheetPlayer.TeamsheetId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(teamsheet => !teamsheet.Deleted);

        builder.HasIndex(teamsheet => teamsheet.FixtureId)
               .IsUnique();
    }
}
