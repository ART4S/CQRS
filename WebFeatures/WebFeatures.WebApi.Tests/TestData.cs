using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.WebApi.Tests
{
    internal static class TestData
    {
        public static async Task SeedDataAsync(IServiceProvider services)
        {
            using IServiceScope scope = services.CreateScope();

            var db = scope.ServiceProvider.GetService<WebFeaturesDbContext>();

            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            var encoder = scope.ServiceProvider.GetService<IPasswordEncoder>();
            var dateTime = scope.ServiceProvider.GetService<IDateTime>();

            #region User and roles

            var usersRole = new Role(ApplicationConstants.Authorization.Roles.Users, "Пользователи");
            await db.Roles.AddAsync(usersRole);

            var user = new User("User", "user@mail.com", encoder.EncodePassword("12345"));
            user.AssignRole(usersRole.Id);

            await db.Users.AddAsync(user);

            var adminRole = new Role(ApplicationConstants.Authorization.Roles.Administrators, "Администраторы");
            await db.Roles.AddAsync(adminRole);

            var admin = new User("Admin", "admin@mail.com", encoder.EncodePassword("12345"));
            admin.AssignRole(adminRole.Id);

            await db.Users.AddAsync(admin);

            #endregion

            #region Products

            var country = new Country("Russia", "EU");
            await db.Countries.AddAsync(country);

            var city = new City("City", country.Id) { Id = Guid.Parse("86b0e7e5-03e2-4aa2-b88b-30c0d8a6a02e") };
            await db.Cities.AddAsync(city);

            var manufacturer = new Manufacturer("Manufacturer", new StreetAddress("Street", "12345", city.Id))
            {
                Id = Guid.Parse("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6")
            };
            await db.Manufacturers.AddAsync(manufacturer);

            var brand = new Brand("Brand") { Id = Guid.Parse("7e6a526d-664e-4b8e-8f55-f78190aa9842") };
            await db.Brands.AddAsync(brand);

            var categories = new[]
            {
                new Category("Man") {Id = Guid.Parse("9ae5bdeb-9df9-4a2c-abd3-68afed9c6561")},
                new Category("Woman"),
                new Category("Shoes"),
                new Category("Watches"),
            };
            await db.Categories.AddRangeAsync(categories);

            var product = new Product("Product", "Description", manufacturer.Id, brand.Id)
            {
                Id = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a"),
                CategoryId = categories[0].Id
            };
            await db.Products.AddAsync(product);

            var review = new Review(user.Id, product.Id, "title", "comment", dateTime.Now, UserRating.OneStar)
            {
                Id = Guid.Parse("a0925263-244b-4f7e-8f3a-80adc2a9835f")
            };
            await db.Reviews.AddAsync(review);

            var comment = new UserComment(product.Id, user.Id, "body", dateTime.Now);
            await db.UserComments.AddAsync(comment);

            var childComment = new UserComment(product.Id, user.Id, "body", dateTime.Now, comment.Id);
            await db.UserComments.AddAsync(childComment);

            #endregion

            await db.SaveChangesAsync();
        }
    }
}