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

            var user = new User("User", "user@mail.com", encoder.EncodePassword("12345"));
            await context.AddAsync(user);

            var admin = new User("Admin", "admin@mail.com", encoder.EncodePassword("12345"));
            await context.AddAsync(admin);

            var usersRole = new Role(AuthorizationConstants.Roles.Users, "Пользователи");
            await context.AddAsync(usersRole);

            var adminsRole = new Role(AuthorizationConstants.Roles.Administrators, "Администраторы");
            await context.AddAsync(adminsRole);

            await context.AddAsync(new UserRoleRelation(user, usersRole));
            await context.AddAsync(new UserRoleRelation(admin, adminsRole));
        }
    }
}
