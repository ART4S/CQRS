using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.WebApi.Tests.Fixtures
{
    public class TestServerFixture
    {
        public TestServer Server { get; }

        public TestServerFixture()
        {
            Server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>());

            SeedData(Server);
        }

        private void SeedData(TestServer server)
        {
            using IServiceScope scope = server.Services.CreateScope();

            var db = scope.ServiceProvider.GetService<WebFeaturesDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var encoder = scope.ServiceProvider.GetService<IPasswordEncoder>();
            var dateTime = scope.ServiceProvider.GetService<IDateTime>();

            #region User and roles

            var usersRole = new Role(ApplicationConstants.Authorization.Roles.Users, "Пользователи");
            db.Roles.Add(usersRole);

            var user = new User("User", "user@mail.com", encoder.EncodePassword("12345"));
            user.AssignRole(usersRole.Id);

            db.Users.Add(user);

            var adminRole = new Role(ApplicationConstants.Authorization.Roles.Administrators, "Администраторы");
            db.Roles.Add(adminRole);

            var admin = new User("Admin", "admin@mail.com", encoder.EncodePassword("12345"));
            admin.AssignRole(adminRole.Id);

            db.Users.Add(admin);

            #endregion

            #region Products

            var country = new Country("Russia", "EU");
            db.Countries.Add(country);

            var city = new City("City", country.Id) { Id = Guid.Parse("86b0e7e5-03e2-4aa2-b88b-30c0d8a6a02e") };
            db.Cities.Add(city);

            var manufacturer = new Manufacturer("Manufacturer", new StreetAddress("Street", "12345", city.Id))
            {
                Id = Guid.Parse("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6")
            };
            db.Manufacturers.Add(manufacturer);

            var brand = new Brand("Brand") { Id = Guid.Parse("7e6a526d-664e-4b8e-8f55-f78190aa9842") };
            db.Brands.Add(brand);

            var categories = new[]
            {
                new Category("Man") { Id = Guid.Parse("9ae5bdeb-9df9-4a2c-abd3-68afed9c6561") },
                new Category("Woman"),
                new Category("Shoes"),
                new Category("Watches"),
            };
            db.Categories.AddRange(categories);

            var product = new Product("Product", "Description", manufacturer.Id, brand.Id)
            {
                Id = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a"),
                CategoryId = categories[0].Id
            };
            db.Products.Add(product);

            var review = new Review(user.Id, product.Id, "title", "comment", dateTime.Now, UserRating.OneStar)
            {
                Id = Guid.Parse("a0925263-244b-4f7e-8f3a-80adc2a9835f")
            };
            db.Reviews.Add(review);

            var comment = new UserComment(product.Id, user.Id, "body", dateTime.Now);
            db.UserComments.Add(comment);

            var childComment = new UserComment(product.Id, user.Id, "body", dateTime.Now, comment.Id);
            db.UserComments.Add(childComment);

            #endregion

            db.SaveChanges();
        }
    }
}