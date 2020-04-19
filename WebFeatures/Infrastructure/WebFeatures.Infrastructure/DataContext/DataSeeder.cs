using System;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Infrastructure
{
    internal static class DataSeeder
    {
        public static void SeedSampleData(BaseDbContext db, IPasswordEncoder encoder, IDateTime dateTime)
        {
            #region Users and roles

            #region Users

            var usersRole = new Role()
            {
                Name = ApplicationConstants.Authorization.Roles.Users,
                Description = "Пользователи"
            };
            db.Roles.Add(usersRole);

            var user = new User()
            {
                Name = "User",
                Email = "user@mail.com",
                PasswordHash = encoder.EncodePassword("12345"),
            };
            db.Users.Add(user);

            user.UserRoles.Add(new UserRole() { RoleId = usersRole.Id, UserId = user.Id });

            #endregion Users 

            #region Administrators

            var adminRole = new Role()
            {
                Name = ApplicationConstants.Authorization.Roles.Administrators,
                Description = "Администраторы"
            };
            db.Roles.Add(adminRole);

            var admin = new User()
            {
                Name = "Admin",
                Email = "admin@mail.com",
                PasswordHash = encoder.EncodePassword("12345")
            };
            db.Users.Add(admin);

            admin.UserRoles.Add(new UserRole() { RoleId = adminRole.Id, UserId = admin.Id });

            #endregion Administrators

            #endregion

            #region Products

            var country = new Country()
            {
                Id = new Guid("f43b588d-2b91-499a-b7e7-2c597f72008f"),
                Name = "Russia",
                Continent = "EU"
            };
            db.Countries.Add(country);

            var city = new City()
            {
                Id = new Guid("86b0e7e5-03e2-4aa2-b88b-30c0d8a6a02e"),
                Name = "City",
                Country = country
            };
            db.Cities.Add(city);

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
            db.Manufacturers.Add(manufacturer);

            var brand = new Brand()
            {
                Id = new Guid("7e6a526d-664e-4b8e-8f55-f78190aa9842"),
                Name = "Brand",
            };
            db.Brands.Add(brand);

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
            db.Categories.AddRange(categories);

            var product = new Product()
            {
                Id = new Guid("0f7b807f-3737-4997-9627-dbe5dc15310a"),
                Name = "Product",
                Description = "Description",
                Manufacturer = manufacturer,
                Category = categories[0],
                Brand = brand
            };
            db.Products.Add(product);

            var review = new Review()
            {
                Id = new Guid("a0925263-244b-4f7e-8f3a-80adc2a9835f"),
                Author = user,
                Product = product,
                Comment = "Comment",
                Title = "Title",
                CreateDate = dateTime.Now,
                Rating = UserRating.FiveStars
            };
            db.Reviews.Add(review);

            var comment = new UserComment()
            {
                Author = user,
                Body = "Comment",
                Product = product,
                CreateDate = dateTime.Now
            };
            db.UserComments.Add(comment);

            var childComment = new UserComment()
            {
                Author = user,
                Body = "Comment",
                Product = product,
                CreateDate = dateTime.Now,
                ParentComment = comment
            };
            db.UserComments.Add(childComment);

            #endregion

            db.SaveChanges();
        }
    }
}
