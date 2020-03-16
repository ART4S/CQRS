using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.DataContext
{
    public static class WebFeaturesDbContextSeed
    {
        public static async Task SeedAsync(this WebFeaturesDbContext context, IServiceProvider services)
        {
            await SeedUserAndRoles(context, services);

            await SeedProducts(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedUserAndRoles(WebFeaturesDbContext context, IServiceProvider services)
        {
            var encoder = services.GetService<IPasswordEncoder>();

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
        }

        private static async Task SeedProducts(WebFeaturesDbContext context)
        {
            var p = new Product("Product 1", "", new Manufacturer("Manufacturer 1", new StreetAddress("StreetAddress", "1", new City("City 1", new Country("Country 1", "")))), new Brand("Calvin Klein"));
            await context.AddAsync(p);
        }
    }
}
