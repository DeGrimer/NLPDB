using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLPDB.Web.Infrastructure;

namespace NLPDB.Web.Data
{
    public static class DataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var isExists = context!.Database.CanConnect();
            if (isExists)
            {
                return;
            }

            await context.Database.MigrateAsync();

            var roles = AppData.Roles.ToArray();
            var roleStore = new RoleStore<IdentityRole>(context);
            foreach (var role in roles)
            {
                if (!context.Roles.Any(x => x.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role) { NormalizedName = role.ToUpper() });
                }
            }

            const string username = "d@f.com";

            if (context.Users.Any(x => x.Email == username))
            {
                return;
            }

            var user = new IdentityUser
            {
                Email = username,
                EmailConfirmed = true,
                NormalizedEmail = username.ToUpper(),
                PhoneNumber = "+7900800553535",
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456");

            var userStore = new UserStore<IdentityUser>(context);
            var identityResult = await userStore.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                throw new ArgumentException();
            }
            var userManger = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            foreach (var role in roles)
            {
                var identityResultRole = await userManger!.AddToRoleAsync(user, role);
                if (!identityResultRole.Succeeded)
                {
                    throw new ArgumentException();
                }
            }

            await context.SaveChangesAsync();
        }
    }
}