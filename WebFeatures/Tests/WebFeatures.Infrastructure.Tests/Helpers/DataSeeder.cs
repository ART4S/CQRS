using Dapper;
using System;
using System.Data;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class DataSeeder
    {
        public static void SeedTestData(IDbConnection connection)
        {
            SeedUsers(connection);
            //SeedProducts(connection);
            //SeedComments(connection);
        }

        private static void SeedUsers(IDbConnection connection)
        {
            var users = new[]
{
                new User()
                {
                    Id = new Guid("0de81728-e359-4925-b94b-acd539e7ad3c"),
                    Name = "Vyacheslav",
                    Email = "vyacheslav@mail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("1dfae12a-c1a6-47aa-b73f-e44e339d16f1"),
                    Name = "Valentin",
                    Email = "valentin@mail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("067d520f-fe3c-493c-a4c9-0bce1cf57212"),
                    Name = "Ilya",
                    Email = "ilya@mail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("a6945683-34e1-46b2-a911-f0c437422b53"),
                    Name = "noname",
                    Email = "noname@mail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("1db40dd2-f7cd-4074-9572-d1748170e71b"),
                    Name = "noname",
                    Email = "test@mail.com",
                    PasswordHash = "hash"
                }
            };

            string sql = "INSERT INTO Users VALUES (@Id, @Name, @Email, @PasswordHash)";

            foreach (User user in users)
            {
                connection.Execute(sql, user);
            }
        }

        private static void SeedBrands(IDbConnection connection)
        {

        }

        private static void SeedProducts(IDbConnection connection)
        {
            var products = new[]
{
                new Product()
                {
                    Id = new Guid("8204d13e-32eb-456c-8161-369c7c86d504"),

                }
            };

            string sql = "INSERT INTO Products VALUES (@Id, @Name, @Email, @PasswordHash)";

            foreach (Product product in products)
            {
                connection.Execute(sql, product);
            }
        }

        private static void SeedComments(IDbConnection connection)
        {
            var comments = new[]
{
                new ProductComment()
                {
                    Id = new Guid("a1cc9c56-15a1-4c33-b222-24471069086c"),
                }
            };

            string sql = "INSERT INTO ProductComments VALUES (@Id, @Name, @Email, @PasswordHash)";

            foreach (ProductComment comment in comments)
            {
                connection.Execute(sql, comment);
            }
        }
    }
}
