using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.MatchTracking.SubstitutionEvents;

internal sealed class SubstitutionEventConfiguration : IEntityTypeConfiguration<SubstitutionEvent>
{
    public void Configure(EntityTypeBuilder<SubstitutionEvent> builder)
    {
        builder.ToTable(Schemas.SubstitutionEvents.Table, Schemas.SubstitutionEvents.Schema);

        builder.HasKey(substitutionEvent => substitutionEvent.Id);

        builder.Property(substitutionEvent => substitutionEvent.Id)
               .HasColumnName(nameof(SubstitutionEvent.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(substitutionEvent => substitutionEvent.FixtureId)
               .HasColumnName(nameof(SubstitutionEvent.FixtureId))
               .IsRequired();

        builder.OwnsOne(substitutionEvent => substitutionEvent.Minute, ownedBuilder =>
        {
            ownedBuilder.Property(minute => minute.BaseMinute)
                        .HasColumnName(nameof(SubstitutionEvent.Minute.BaseMinute))
                        .IsRequired();

            ownedBuilder.Property(minute => minute.StoppageMinute)
                        .HasColumnName(nameof(SubstitutionEvent.Minute.StoppageMinute));
        });

        builder.Property(substitutionEvent => substitutionEvent.OccurredAtUtc)
               .HasColumnName(nameof(SubstitutionEvent.OccurredAtUtc))
               .IsRequired();

        builder.OwnsOne(substitutionEvent => substitutionEvent.Substitution, ownedBuilder =>
        {
            ownedBuilder.Property(substitution => substitution.PlayerOffId)
                        .HasColumnName(nameof(Substitution.PlayerOffId))
                        .IsRequired();

            ownedBuilder.HasOne<Player>()
                        .WithMany()
                        .HasForeignKey(substitution => substitution.PlayerOffId)
                        .OnDelete(DeleteBehavior.NoAction);

            ownedBuilder.Property(substitution => substitution.PlayerOnId)
                        .HasColumnName(nameof(Substitution.PlayerOnId))
                        .IsRequired();

            ownedBuilder.HasOne<Player>()
                        .WithMany()
                        .HasForeignKey(substitution => substitution.PlayerOnId)
                        .OnDelete(DeleteBehavior.NoAction);

            ownedBuilder.WithOwner();
        });

        builder.Property(substitutionEvent => substitutionEvent.DeletedOnUtc)
               .HasColumnName(nameof(SubstitutionEvent.DeletedOnUtc));

        builder.Property(substitutionEvent => substitutionEvent.Deleted)
               .HasColumnName(nameof(SubstitutionEvent.Deleted))
               .HasDefaultValue(false);

        builder.HasQueryFilter(substitutionEvent => !substitutionEvent.Deleted);
    }
}
