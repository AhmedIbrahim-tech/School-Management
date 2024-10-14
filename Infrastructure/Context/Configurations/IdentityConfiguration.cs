using Data.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context.Configurations;

public class IdentityConfiguration : IEntityTypeConfiguration<User>,
                                     IEntityTypeConfiguration<Role>,
                                     IEntityTypeConfiguration<IdentityUserRole<int>>,
                                     IEntityTypeConfiguration<IdentityUserClaim<int>>,
                                     IEntityTypeConfiguration<IdentityUserLogin<int>>,
                                     IEntityTypeConfiguration<IdentityRoleClaim<int>>,
                                     IEntityTypeConfiguration<IdentityUserToken<int>>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
    }

    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.ToTable("UserRoles");
    }

    public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
    {
        builder.ToTable("UserClaims");
    }

    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder.ToTable("UserLogins");
    }

    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        builder.ToTable("RoleClaims");
    }

    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder.ToTable("UserTokens");
    }

}
