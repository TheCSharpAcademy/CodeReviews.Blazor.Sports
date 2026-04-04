namespace SportsStatistics.Authorization.Schemas;

internal static class IdentitySchema
{
    internal sealed record SchemaMetadata(string Schema, string Table);

    private const string Schema = "identity";

    public static readonly SchemaMetadata ApplicationUser = new(Schema, "Users");
    public static readonly SchemaMetadata IdentityRole = new(Schema, "Roles");
    public static readonly SchemaMetadata IdentityUserClaim = new(Schema, "UserClaims");
    public static readonly SchemaMetadata IdentityUserLogin = new(Schema, "UserLogin");
    public static readonly SchemaMetadata IdentityUserRole = new(Schema, "UserRoles");
    public static readonly SchemaMetadata IdentityUserToken = new(Schema, "UserTokens");
    public static readonly SchemaMetadata IdentityRoleClaim = new(Schema, "RoleClaims");
    public static readonly SchemaMetadata MigrationsHistory = new(Schema, "__EFMigrationsHistory");
}
