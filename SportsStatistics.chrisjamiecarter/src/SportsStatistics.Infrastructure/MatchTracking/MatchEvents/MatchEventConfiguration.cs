using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.MatchTracking.MatchEvents;

internal sealed class MatchEventConfiguration : IEntityTypeConfiguration<MatchEvent>
{
    public void Configure(EntityTypeBuilder<MatchEvent> builder)
    {
        builder.ToTable(Schemas.MatchEvents.Table, Schemas.MatchEvents.Schema);

        builder.HasKey(matchEvent => matchEvent.Id);

        builder.Property(matchEvent => matchEvent.Id)
               .HasColumnName(nameof(MatchEvent.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(matchEvent => matchEvent.FixtureId)
               .HasColumnName(nameof(MatchEvent.FixtureId))
               .IsRequired();

        builder.OwnsOne(matchEvent => matchEvent.Minute, ownedBuilder =>
        {
            ownedBuilder.Property(minute => minute.BaseMinute)
                        .HasColumnName(nameof(MatchEvent.Minute.BaseMinute))
                        .IsRequired();

            ownedBuilder.Property(minute => minute.StoppageMinute)
                        .HasColumnName(nameof(MatchEvent.Minute.StoppageMinute));
        });

        builder.Property(matchEvent => matchEvent.OccurredAtUtc)
               .HasColumnName(nameof(MatchEvent.OccurredAtUtc))
               .IsRequired();

        builder.Property(matchEvent => matchEvent.Type)
               .HasColumnName(nameof(MatchEvent.Type))
               .IsRequired();

        builder.Property(matchEvent => matchEvent.DeletedOnUtc)
               .HasColumnName(nameof(MatchEvent.DeletedOnUtc));

        builder.Property(matchEvent => matchEvent.Deleted)
               .HasColumnName(nameof(MatchEvent.Deleted))
               .HasDefaultValue(false);

        builder.HasOne<Fixture>()
               .WithMany()
               .HasForeignKey(matchEvent => matchEvent.FixtureId);

        builder.HasQueryFilter(matchEvent => !matchEvent.Deleted);
    }
}
