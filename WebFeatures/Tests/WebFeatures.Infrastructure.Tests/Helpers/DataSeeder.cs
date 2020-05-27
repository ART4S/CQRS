using System.Data;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.Tests.Data;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class DataSeeder
    {
        public static void SeedTestData(IDbConnection connection)
        {
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                SeedUsers(connection);
                SeedRoles(connection);
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

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        private static void SeedUsers(IDbConnection connection)
        {
            foreach (User user in InitialDbData.Users)
            {
                SqlQuery sql = SqlBuilder.InsertUser(user);
                connection.Execute(sql);
            }
        }

        private static void SeedRoles(IDbConnection connection)
        {
            foreach (Role role in InitialDbData.Roles)
            {
                SqlQuery sql = SqlBuilder.InsertRole(role);
                connection.Execute(sql);
            }
        }

        private static void SeedUserRoles(IDbConnection connection)
        {
            foreach (UserRole userRole in InitialDbData.UserRoles)
            {
                SqlQuery sql = SqlBuilder.InsertUserRole(userRole);
                connection.Execute(sql);
            }
        }

        private static void SeedCountries(IDbConnection connection)
        {
            foreach (Country country in InitialDbData.Countries)
            {
                SqlQuery sql = SqlBuilder.InsertCountry(country);
                connection.Execute(sql);
            }
        }

        private static void SeedCities(IDbConnection connection)
        {
            foreach (City city in InitialDbData.Cities)
            {
                SqlQuery sql = SqlBuilder.InsertCity(city);
                connection.Execute(sql);
            }
        }

        private static void SeedBrands(IDbConnection connection)
        {
            foreach (Brand brand in InitialDbData.Brands)
            {
                SqlQuery sql = SqlBuilder.InsertBrand(brand);
                connection.Execute(sql);
            }
        }

        private static void SeedCategories(IDbConnection connection)
        {
            foreach (Category category in InitialDbData.Categories)
            {
                SqlQuery sql = SqlBuilder.InsertCategory(category);
                connection.Execute(sql);
            }
        }

        private static void SeedManufacturers(IDbConnection connection)
        {
            foreach (Manufacturer manufacturer in InitialDbData.Manufacturers)
            {
                SqlQuery sql = SqlBuilder.InsertManufacturer(manufacturer);
                connection.Execute(sql);
            }
        }

        private static void SeedShippers(IDbConnection connection)
        {
            foreach (Shipper shipper in InitialDbData.Shippers)
            {
                SqlQuery sql = SqlBuilder.InsertShipper(shipper);
                connection.Execute(sql);
            }
        }

        private static void SeedProducts(IDbConnection connection)
        {
            foreach (Product product in InitialDbData.Products)
            {
                SqlQuery sql = SqlBuilder.InsertProduct(product);
                connection.Execute(sql);
            }
        }

        private static void SeedProductReviews(IDbConnection connection)
        {
            foreach (ProductReview review in InitialDbData.ProductReviews)
            {
                SqlQuery sql = SqlBuilder.InsertProductReview(review);
                connection.Execute(sql);
            }
        }

        private static void SeedProductComments(IDbConnection connection)
        {
            foreach (ProductComment comment in InitialDbData.ProductComments)
            {
                SqlQuery sql = SqlBuilder.InsertProductComment(comment);
                connection.Execute(sql);
            }
        }
    }
}
