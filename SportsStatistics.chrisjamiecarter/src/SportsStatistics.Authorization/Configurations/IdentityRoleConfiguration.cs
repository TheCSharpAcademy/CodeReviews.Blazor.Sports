using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Authorization.Entities;
using SportsStatistics.Authorization.Schemas;

namespace SportsStatistics.Authorization.Configurations;

internal sealed class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.ToTable(IdentitySchema.IdentityRole.Table, IdentitySchema.IdentityRole.Schema);
    }
}
