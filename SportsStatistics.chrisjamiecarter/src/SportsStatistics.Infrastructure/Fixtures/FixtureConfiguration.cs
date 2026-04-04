using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Fixtures;

internal sealed class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.ToTable(Schemas.Fixtures.Table, Schemas.Fixtures.Schema);

        builder.HasKey(fixture => fixture.Id);

        builder.Property(fixture => fixture.Id)
               .HasColumnName(nameof(Fixture.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(fixture => fixture.CompetitionId)
               .HasColumnName(nameof(Fixture.CompetitionId))
               .IsRequired();

        builder.ComplexProperty(fixture => fixture.Opponent, complexBuilder =>
        {
            complexBuilder.Property(opponent => opponent.Value)
                          .HasColumnName(nameof(Fixture.Opponent))
                          .HasMaxLength(Opponent.MaxLength)
                          .IsRequired();
        });

        builder.ComplexProperty(fixture => fixture.KickoffTimeUtc, complexBuilder =>
        {
            complexBuilder.Property(kickoffTimeUtc => kickoffTimeUtc.Value)
                          .HasColumnName(nameof(Fixture.KickoffTimeUtc))
                          .IsRequired();
        });

        builder.Property(fixture => fixture.Location)
               .HasColumnName(nameof(Fixture.Location))
               .IsRequired();

        builder.OwnsOne(fixture => fixture.Score, ownedBuilder =>
        {
            ownedBuilder.Property(score => score.HomeGoals)
                        .HasColumnName(nameof(Score.HomeGoals))
                        .IsRequired();

            ownedBuilder.Property(score => score.AwayGoals)
                        .HasColumnName(nameof(Score.AwayGoals))
                        .IsRequired();

            ownedBuilder.WithOwner();
        });

        builder.Property(fixture => fixture.Status)
               .HasColumnName(nameof(Fixture.Status))
               .IsRequired();

        builder.Property(fixture => fixture.DeletedOnUtc)
               .HasColumnName(nameof(Fixture.DeletedOnUtc));

        builder.Property(fixture => fixture.Deleted)
               .HasColumnName(nameof(Fixture.Deleted))
               .HasDefaultValue(false);

        builder.HasOne<Competition>()
               .WithMany()
               .HasForeignKey(fixture => fixture.CompetitionId);

        builder.HasQueryFilter(fixture => !fixture.Deleted);
    }
}
