using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(Schemas.Players.Table, Schemas.Players.Schema);

        builder.HasKey(player => player.Id);

        builder.Property(player => player.Id)
               .HasColumnName(nameof(Player.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.ComplexProperty(player => player.Name, complexBuilder =>
        {
            complexBuilder.Property(name => name.Value)
                          .HasColumnName(nameof(Player.Name))
                          .HasMaxLength(Name.MaxLength)
                          .IsRequired();
        });

        builder.ComplexProperty(player => player.SquadNumber, complexBuilder =>
        {
            complexBuilder.Property(squadNumber => squadNumber.Value)
                          .HasColumnName(nameof(Player.SquadNumber))
                          .IsRequired();
        });

        builder.ComplexProperty(player => player.Nationality, complexBuilder =>
        {
            complexBuilder.Property(nationality => nationality.Value)
                          .HasColumnName(nameof(Player.Nationality))
                          .HasMaxLength(Nationality.MaxLength)
                          .IsRequired();
        });

        builder.ComplexProperty(player => player.DateOfBirth, complexBuilder =>
        {
            complexBuilder.Property(dateOfBirth => dateOfBirth.Value)
                          .HasColumnName(nameof(Player.DateOfBirth))
                          .IsRequired();
        });

        builder.Property(player => player.Position)
               .HasColumnName(nameof(Player.Position))
               .IsRequired();

        builder.Property(player => player.LeftClub)
               .HasColumnName(nameof(Player.LeftClub))
               .HasDefaultValue(false);
        
        builder.Property(player => player.LeftClubOnUtc)
               .HasColumnName(nameof(Player.LeftClubOnUtc));
     
        builder.Ignore(player => player.Age);
    }
}
