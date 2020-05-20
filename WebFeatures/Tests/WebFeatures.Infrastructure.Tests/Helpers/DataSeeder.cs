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
        }

        private static void SeedUsers(IDbConnection connection)
        {
            var users = new[]
{
                new User()
                {
                    Id = new Guid("0de81728-e359-4925-b94b-acd539e7ad3c"),
                    Name = "Vyacheslav",
                    Email = "vyacheslav@gmail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("1dfae12a-c1a6-47aa-b73f-e44e339d16f1"),
                    Name = "Valentin",
                    Email = "valentin@gmail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("067d520f-fe3c-493c-a4c9-0bce1cf57212"),
                    Name = "Ilya",
                    Email = "ilya@gmail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("a6945683-34e1-46b2-a911-f0c437422b53"),
                    Name = "noname",
                    Email = "noname@gmail.com",
                    PasswordHash = "hash"
                },
                new User()
                {
                    Id = new Guid("1db40dd2-f7cd-4074-9572-d1748170e71b"),
                    Name = "userWithEmail",
                    Email = "test@gmail.com",
                    PasswordHash = "hash"
                }
            };

            string sql = "INSERT INTO Users VALUES (@Id, @Name, @Email, @PasswordHash)";

            foreach (User user in users)
            {
                connection.Execute(sql, user);
            }
        }
    }
}
