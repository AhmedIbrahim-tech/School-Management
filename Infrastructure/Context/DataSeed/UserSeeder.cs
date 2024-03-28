using Data.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Context.DataSeed;

public static class UserSeeder
{
    public static async Task SeedAsync(UserManager<User> _userManager)
    {
        var usersCount = await _userManager.Users.CountAsync();
        if (usersCount <= 0)
        {
            var defaultuser = new User()
            {
                UserName = "AhmedEprahim",
                Email = "admin@project.com",
                FullName = "AhmedEprahim",
                Country = "Egypt",
                PhoneNumber = "123456",
                Address = "Egypt",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            await _userManager.CreateAsync(defaultuser, "123456");
            await _userManager.AddToRoleAsync(defaultuser, "Admin");
        }
    }
}
