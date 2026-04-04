using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.MatchTracking.PlayerEvents;

internal sealed class PlayerEventConfiguration : IEntityTypeConfiguration<PlayerEvent>
{
    public void Configure(EntityTypeBuilder<PlayerEvent> builder)
    {
        builder.ToTable(Schemas.PlayerEvents.Table, Schemas.PlayerEvents.Schema);

        builder.HasKey(playerEvent => playerEvent.Id);

        builder.Property(playerEvent => playerEvent.Id)
               .HasColumnName(nameof(PlayerEvent.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(playerEvent => playerEvent.FixtureId)
               .HasColumnName(nameof(PlayerEvent.FixtureId))
               .IsRequired();

        builder.OwnsOne(playerEvent => playerEvent.Minute, ownedBuilder =>
        {
            ownedBuilder.Property(minute => minute.BaseMinute)
                        .HasColumnName(nameof(PlayerEvent.Minute.BaseMinute))
                        .IsRequired();

            ownedBuilder.Property(minute => minute.StoppageMinute)
                        .HasColumnName(nameof(PlayerEvent.Minute.StoppageMinute));
        });

        builder.Property(playerEvent => playerEvent.OccurredAtUtc)
               .HasColumnName(nameof(PlayerEvent.OccurredAtUtc))
               .IsRequired();

        builder.Property(playerEvent => playerEvent.PlayerId)
               .HasColumnName(nameof(PlayerEvent.PlayerId))
               .IsRequired();

        builder.Property(playerEvent => playerEvent.Type)
               .HasColumnName(nameof(PlayerEvent.Type))
               .IsRequired();

        builder.Property(playerEvent => playerEvent.DeletedOnUtc)
               .HasColumnName(nameof(PlayerEvent.DeletedOnUtc));

        builder.Property(playerEvent => playerEvent.Deleted)
               .HasColumnName(nameof(PlayerEvent.Deleted))
               .HasDefaultValue(false);

        builder.HasOne<Fixture>()
               .WithMany()
               .HasForeignKey(playerEvent => playerEvent.FixtureId);

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(playerEvent => playerEvent.PlayerId);

        builder.HasQueryFilter(playerEvent => !playerEvent.Deleted);
    }
}
