using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.ReadContext;
using WebFeatures.WriteContext;

namespace WebFeatures.WebApi.Tests.Fixtures
{
    public class TestServerFixture
    {
        public TestServer Server { get; }

        public TestServerFixture()
        {
            Server = new TestServer(
                new WebHostBuilder()
                .ConfigureAppConfiguration((ctx, config) =>
                {
                    config.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json");
                })
                .UseStartup<Startup>()
                .UseEnvironment("Testing"));

            SeedData(Server);
        }

        private void SeedData(TestServer server)
        {
            using IServiceScope scope = server.Services.CreateScope();

            var writeDb = scope.ServiceProvider.GetService<EFWriteContext>();

            writeDb.Database.EnsureDeleted();
            writeDb.Database.EnsureCreated();

            var readDb = scope.ServiceProvider.GetService<MongoDbReadContext>();

            string readDbName = readDb.Database.DatabaseNamespace.DatabaseName;
            readDb.Database.Client.DropDatabase(readDbName);

            var encoder = scope.ServiceProvider.GetService<IPasswordEncoder>();
            var dateTime = scope.ServiceProvider.GetService<IDateTime>();

            #region User and roles

            var usersRole = new Role()
            {
                Name = ApplicationConstants.Authorization.Roles.Users,
                Description = "Пользователи"
            };
            writeDb.Roles.Add(usersRole);

            var user = new User()
            {
                Name = "User",
                Email = "user@mail.com",
                PasswordHash = encoder.EncodePassword("12345"),
            };
            user.UserRoles.Add(new UserRole() { Role = usersRole });

            writeDb.Users.Add(user);

            var adminRole = new Role()
            {
                Name = ApplicationConstants.Authorization.Roles.Administrators,
                Description = "Администраторы"
            };
            writeDb.Roles.Add(adminRole);

            var admin = new User()
            {
                Name = "Admin",
                Email = "admin@mail.com",
                PasswordHash = encoder.EncodePassword("12345")
            };
            user.UserRoles.Add(new UserRole() { Role = adminRole });

            writeDb.Users.Add(admin);

            #endregion

            #region Products

            var country = new Country()
            {
                Id = new Guid("f43b588d-2b91-499a-b7e7-2c597f72008f"),
                Name = "Russia",
                Continent = "EU"
            };
            writeDb.Countries.Add(country);

            var city = new City()
            {
                Id = new Guid("86b0e7e5-03e2-4aa2-b88b-30c0d8a6a02e"),
                Name = "City",
                Country = country
            };
            writeDb.Cities.Add(city);

            var manufacturer = new Manufacturer()
            {
                Id = new Guid("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6"),
                OrganizationName = "Manufacturer",
                StreetAddress = new StreetAddress()
                {
                    StreetName = "Street",
                    PostalCode = "12345",
                    City = city
                }
            };
            writeDb.Manufacturers.Add(manufacturer);

            var brand = new Brand()
            {
                Id = Guid.Parse("7e6a526d-664e-4b8e-8f55-f78190aa9842"),
                Name = "Brand",
            };
            writeDb.Brands.Add(brand);

            var categories = new[]
            {
                new Category()
                {
                    Id = new Guid("9ae5bdeb-9df9-4a2c-abd3-68afed9c6561"),
                    Name = "Man"
                },
                new Category() { Name = "Woman" },
                new Category() { Name = "Shoes" },
                new Category() { Name = "Watches" },
            };
            writeDb.Categories.AddRange(categories);

            var product = new Product()
            {
                Id = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a"),
                Name = "Product",
                Description = "Description",
                Manufacturer = manufacturer,
                Category = categories[0],
                Brand = brand
            };
            writeDb.Products.Add(product);
            readDb.Add(product);

            var review = new Review()
            {
                Id = Guid.Parse("a0925263-244b-4f7e-8f3a-80adc2a9835f"),
                Author = user,
                Product = product,
                Comment = "Comment",
                Title = "Title",
                CreateDate = dateTime.Now,
                Rating = UserRating.FiveStars
            };
            writeDb.Reviews.Add(review);
            readDb.Add(review);

            var comment = new UserComment()
            {
                Author = user,
                Body = "Comment",
                Product = product,
                CreatedAt = dateTime.Now
            };
            writeDb.UserComments.Add(comment);
            readDb.Add(comment);

            var childComment = new UserComment()
            {
                Author = user,
                Body = "Comment",
                Product = product,
                CreatedAt = dateTime.Now,
                ParentComment = comment
            };
            writeDb.UserComments.Add(childComment);
            readDb.Add(childComment);

            #endregion

            writeDb.SaveChanges();
        }
    }
}