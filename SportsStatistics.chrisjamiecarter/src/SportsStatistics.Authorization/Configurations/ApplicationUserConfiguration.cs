using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Authorization.Entities;
using SportsStatistics.Authorization.Schemas;

namespace SportsStatistics.Authorization.Configurations;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(IdentitySchema.ApplicationUser.Table, IdentitySchema.ApplicationUser.Schema);
    }
}
