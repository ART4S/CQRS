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

            var usersRole = new Role(ApplicationConstants.Authorization.Roles.Users, "Пользователи");
            await context.AddAsync(usersRole);

            var user = new User("User", "user@mail.com", encoder.EncodePassword("12345"));
            user.AssignRole(usersRole.Id);

            await context.AddAsync(user);

            var adminsRole = new Role(ApplicationConstants.Authorization.Roles.Administrators, "Администраторы");
            await context.AddAsync(adminsRole);

            var admin = new User("Admin", "admin@mail.com", encoder.EncodePassword("12345"));
            admin.AssignRole(adminsRole.Id);

            await context.AddAsync(admin);
        }

        private static async Task SeedProducts(WebFeaturesDbContext context)
        {
            var country = new Country("Russia", "EU") { Id = Guid.Parse("ab9d72d4-8e0a-4278-84c7-3beef9b380ac") };
            await context.AddAsync(country);

            var city = new City("City", country.Id) { Id = Guid.Parse("86b0e7e5-03e2-4aa2-b88b-30c0d8a6a02e") };
            await context.AddAsync(city);

            var manufacturer = new Manufacturer("Manufacturer", new StreetAddress("Street", "12345", city.Id))
            {
                Id = Guid.Parse("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6")
            };
            await context.AddAsync(manufacturer);

            var brand = new Brand("Brand") { Id = Guid.Parse("7e6a526d-664e-4b8e-8f55-f78190aa9842") };
            await context.AddAsync(brand);

            var category = new Category("Category") { Id = Guid.Parse("6d5848a0-6b79-4c94-84e1-f85e72968b03") };
            await context.AddAsync(category);

            var product = new Product("Product", "Description", manufacturer.Id, brand.Id)
            {
                Id = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a"),
                CategoryId = category.Id
            };
            await context.AddAsync(product);
        }
    }
}
