using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Teamsheets;

internal sealed class TeamsheetPlayerConfiguration : IEntityTypeConfiguration<TeamsheetPlayer>
{
    public void Configure(EntityTypeBuilder<TeamsheetPlayer> builder)
    {
        builder.ToTable(Schemas.TeamsheetPlayers.Table, Schemas.TeamsheetPlayers.Schema);

        builder.HasKey(teamsheetPlayer => teamsheetPlayer.Id);

        builder.Property(teamsheetPlayer => teamsheetPlayer.Id)
               .HasColumnName(nameof(TeamsheetPlayer.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(teamsheetPlayer => teamsheetPlayer.TeamsheetId)
               .HasColumnName(nameof(TeamsheetPlayer.TeamsheetId))
               .IsRequired();

        builder.Property(teamsheetPlayer => teamsheetPlayer.PlayerId)
               .HasColumnName(nameof(TeamsheetPlayer.PlayerId))
               .IsRequired();

        builder.Property(teamsheetPlayer => teamsheetPlayer.IsStarter)
               .HasColumnName(nameof(TeamsheetPlayer.IsStarter))
               .IsRequired();

        builder.HasIndex(teamsheetPlayer => new { teamsheetPlayer.TeamsheetId, teamsheetPlayer.PlayerId })
               .IsUnique();
    }
}
