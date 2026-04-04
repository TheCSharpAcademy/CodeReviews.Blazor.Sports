using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Clubs;

internal sealed class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable(Schemas.Clubs.Table, Schemas.Clubs.Schema);

        builder.HasKey(club => club.Id);

        builder.Property(club => club.Id)
               .HasColumnName(nameof(Club.Id))
               .IsRequired()
               .ValueGeneratedNever();

        builder.ComplexProperty(club => club.Name, complexBuilder =>
        {
            complexBuilder.Property(name => name.Value)
                          .HasColumnName(nameof(Club.Name))
                          .HasMaxLength(Name.MaxLength)
                          .IsRequired();
        });
    }
}
