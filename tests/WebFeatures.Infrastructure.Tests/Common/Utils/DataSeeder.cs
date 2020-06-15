using System.Data;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Infrastructure.Tests.Common.Data;

namespace WebFeatures.Infrastructure.Tests.Common.Utils
{
    internal static class DataSeeder
    {
        public static void SeedTestData(IDbConnection connection)
        {
            SeedUsers(connection);
            SeedRoles(connection);
            SeedRolePermissions(connection);
            SeedUserRoles(connection);
            SeedCountries(connection);
            SeedCities(connection);
            SeedBrands(connection);
            SeedCategories(connection);
            SeedManufacturers(connection);
            SeedShippers(connection);
            SeedProducts(connection);
            SeedProductReviews(connection);
            SeedProductComments(connection);
        }

        private static void SeedUsers(IDbConnection connection)
        {
            foreach (User user in InitialData.Users)
            {
                SqlQuery sql = SqlBuilder.InsertUser(user);
                connection.Execute(sql);
            }
        }

        private static void SeedRoles(IDbConnection connection)
        {
            foreach (Role role in InitialData.Roles)
            {
                SqlQuery sql = SqlBuilder.InsertRole(role);
                connection.Execute(sql);
            }
        }

        private static void SeedRolePermissions(IDbConnection connection)
        {
            foreach (RolePermission permission in InitialData.RolePermissions)
            {
                SqlQuery sql = SqlBuilder.InsertRolePermission(permission);
                connection.Execute(sql);
            }
        }

        private static void SeedUserRoles(IDbConnection connection)
        {
            foreach (UserRole userRole in InitialData.UserRoles)
            {
                SqlQuery sql = SqlBuilder.InsertUserRole(userRole);
                connection.Execute(sql);
            }
        }

        private static void SeedCountries(IDbConnection connection)
        {
            foreach (Country country in InitialData.Countries)
            {
                SqlQuery sql = SqlBuilder.InsertCountry(country);
                connection.Execute(sql);
            }
        }

        private static void SeedCities(IDbConnection connection)
        {
            foreach (City city in InitialData.Cities)
            {
                SqlQuery sql = SqlBuilder.InsertCity(city);
                connection.Execute(sql);
            }
        }

        private static void SeedBrands(IDbConnection connection)
        {
            foreach (Brand brand in InitialData.Brands)
            {
                SqlQuery sql = SqlBuilder.InsertBrand(brand);
                connection.Execute(sql);
            }
        }

        private static void SeedCategories(IDbConnection connection)
        {
            foreach (Category category in InitialData.Categories)
            {
                SqlQuery sql = SqlBuilder.InsertCategory(category);
                connection.Execute(sql);
            }
        }

        private static void SeedManufacturers(IDbConnection connection)
        {
            foreach (Manufacturer manufacturer in InitialData.Manufacturers)
            {
                SqlQuery sql = SqlBuilder.InsertManufacturer(manufacturer);
                connection.Execute(sql);
            }
        }

        private static void SeedShippers(IDbConnection connection)
        {
            foreach (Shipper shipper in InitialData.Shippers)
            {
                SqlQuery sql = SqlBuilder.InsertShipper(shipper);
                connection.Execute(sql);
            }
        }

        private static void SeedProducts(IDbConnection connection)
        {
            foreach (Product product in InitialData.Products)
            {
                SqlQuery sql = SqlBuilder.InsertProduct(product);
                connection.Execute(sql);
            }
        }

        private static void SeedProductReviews(IDbConnection connection)
        {
            foreach (ProductReview review in InitialData.ProductReviews)
            {
                SqlQuery sql = SqlBuilder.InsertProductReview(review);
                connection.Execute(sql);
            }
        }

        private static void SeedProductComments(IDbConnection connection)
        {
            foreach (ProductComment comment in InitialData.ProductComments)
            {
                SqlQuery sql = SqlBuilder.InsertProductComment(comment);
                connection.Execute(sql);
            }
        }
    }
}
