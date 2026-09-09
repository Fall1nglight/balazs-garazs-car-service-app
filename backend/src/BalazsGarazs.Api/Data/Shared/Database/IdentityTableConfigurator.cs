using BalazsGarazs.Api.Data.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BalazsGarazs.Api.Data.Shared.Database;

public static class IdentityTableConfigurator
{
    public static void ConfigureIdentityTables(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable(IdentityTableNames.Users, AppDbContextSchemas.Identity);

        builder
            .Entity<IdentityRole<Guid>>()
            .ToTable(IdentityTableNames.Roles, AppDbContextSchemas.Identity);

        builder
            .Entity<IdentityUserRole<Guid>>()
            .ToTable(IdentityTableNames.UserRoles, AppDbContextSchemas.Identity);

        builder
            .Entity<IdentityUserClaim<Guid>>()
            .ToTable(IdentityTableNames.UserClaims, AppDbContextSchemas.Identity);

        builder
            .Entity<IdentityUserLogin<Guid>>()
            .ToTable(IdentityTableNames.UserLogins, AppDbContextSchemas.Identity);

        builder
            .Entity<IdentityRoleClaim<Guid>>()
            .ToTable(IdentityTableNames.RoleClaims, AppDbContextSchemas.Identity);

        builder
            .Entity<IdentityUserToken<Guid>>()
            .ToTable(IdentityTableNames.UserTokens, AppDbContextSchemas.Identity);
    }
}
