using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Authorization.Schemas;

namespace SportsStatistics.Authorization.Configurations;

internal sealed class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable(IdentitySchema.IdentityUserClaim.Table, IdentitySchema.IdentityUserClaim.Schema);
    }
}
