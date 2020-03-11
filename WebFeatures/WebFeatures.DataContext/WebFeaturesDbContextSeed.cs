using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext
{
    public static class WebFeaturesDbContextSeed
    {
        public static async Task SeedAsync(this WebFeaturesDbContext context, IServiceProvider provider)
        {
            var encoder = provider.GetService<IPasswordEncoder>();

            var usersRole = new Role(AuthorizationConstants.Roles.Users, "Пользователи");
            await context.AddAsync(usersRole);

            var user = new User("User", "user@mail.com", encoder.EncodePassword("12345"));
            user.AssignRole(usersRole);

            await context.AddAsync(user);

            var adminsRole = new Role(AuthorizationConstants.Roles.Administrators, "Администраторы");
            await context.AddAsync(adminsRole);

            var admin = new User("Admin", "admin@mail.com", encoder.EncodePassword("12345"));
            admin.AssignRole(adminsRole);

            await context.AddAsync(admin);

            await context.SaveChangesAsync();
        }
    }
}
