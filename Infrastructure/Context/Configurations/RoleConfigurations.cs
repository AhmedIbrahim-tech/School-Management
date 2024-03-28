using Data.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Context.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role { Name = "Admin" },
                 new Role { Name = "User" }
            );
    }
}