using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Identity.Model;

namespace WebFeatures.Identity
{
    public static class ApplicationDbContextSeed
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = AuthorizationConstants.Roles.Administrators,
                Description = "Администраторы"
            });

            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = AuthorizationConstants.Roles.Users,
                Description = "Пользователи",
            });

            var user = new ApplicationUser()
            {
                UserName = "user", 
                Email = "user@gmail.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "12345");
            user = await userManager.FindByIdAsync(user.Id);
            await userManager.AddToRoleAsync(user, AuthorizationConstants.Roles.Users);

            var admin = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "12345");
            admin = await userManager.FindByIdAsync(admin.Id);
            await userManager.AddToRoleAsync(admin, AuthorizationConstants.Roles.Administrators);
        }
    }
}
