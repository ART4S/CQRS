using Dapper;
using System.Data;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class DataSeeder
    {
        public static void SeedTestData(IDbConnection connection)
        {
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                Seed(connection, TestData.Users);
                Seed(connection, TestData.Roles);
                Seed(connection, TestData.UserRoles);
                Seed(connection, TestData.Countries);
                Seed(connection, TestData.Cities);
                Seed(connection, TestData.Addresses);
                Seed(connection, TestData.Brands);
                Seed(connection, TestData.Categories);
                Seed(connection, TestData.Manufacturers);
                Seed(connection, TestData.Shippers);
                Seed(connection, TestData.Products);
                Seed(connection, TestData.ProductReviews);
                Seed(connection, TestData.ProductComments);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        private static void Seed<TEntity>(IDbConnection connection, TEntity[] entities) where TEntity : class
        {
            string sql = SqlBuilder.Insert<TEntity>();

            foreach (TEntity entity in entities)
            {
                connection.Execute(sql, entity);
            }
        }
    }
}
