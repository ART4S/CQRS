using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class TestData
    {
        /// <summary>
        ///  Пользователи
        /// </summary>
        public static readonly User[] Users = new[]
        {
            new User()
            {
                Id = new Guid("0de81728-e359-4925-b94b-acd539e7ad3c"),
                Name = "Name_1",
                Email = "Email_1",
                PasswordHash = "PasswordHash_1"
            },
            new User()
            {
                Id = new Guid("1dfae12a-c1a6-47aa-b73f-e44e339d16f1"),
                Name = "Name_2",
                Email = "Email_2",
                PasswordHash = "PasswordHash_2"
            },
            new User()
            {
                Id = new Guid("067d520f-fe3c-493c-a4c9-0bce1cf57212"),
                Name = "Name_3",
                Email = "Email_3",
                PasswordHash = "PasswordHash_3"
            },
            new User()
            {
                Id = new Guid("a6945683-34e1-46b2-a911-f0c437422b53"),
                Name = "Name_4",
                Email = "Email_4",
                PasswordHash = "PasswordHash_4"
            },
            new User()
            {
                Id = new Guid("1db40dd2-f7cd-4074-9572-d1748170e71b"),
                Name = "Name_5",
                Email = "Email_5",
                PasswordHash = "PasswordHash_5"
            },
            new User()
            {
                Id = new Guid("7f46fdda-8ab7-48eb-b713-2970b5038485"),
                Name = "Name_6",
                Email = "Email_6",
                PasswordHash = "PasswordHash_6"
            },
            new User()
            {
                Id = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03"),
                Name = "Name_7",
                Email = "test@mail.com",
                PasswordHash = "PasswordHash_7"
            }
        };

        /// <summary>
        /// Роли
        /// </summary>
        public static readonly Role[] Roles = new[]
        {
            new Role()
            {
                Id = new Guid("481bd67e-4177-457d-aaa5-f002b0cd8a6f"),
                Name = "Name_1"
            },
            new Role()
            {
                Id = new Guid("21ab9b3d-e971-45ca-8f3e-5a68abb82a6c"),
                Name = "Name_2"
            },
            new Role()
            {
                Id = new Guid("5c198568-5e41-46aa-9a4b-1565c66100e7"),
                Name = "Name_3"
            }
        };

        /// <summary>
        /// Роли пользователей
        /// </summary>
        public static readonly UserRole[] UserRoles = new[]
        {
            new UserRole()
            {
                UserId = new Guid("7f46fdda-8ab7-48eb-b713-2970b5038485"),
                RoleId = new Guid("21ab9b3d-e971-45ca-8f3e-5a68abb82a6c")
            },
            new UserRole()
            {
                UserId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03"),
                RoleId = new Guid("481bd67e-4177-457d-aaa5-f002b0cd8a6f")
            },
            new UserRole()
            {
                UserId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03"),
                RoleId = new Guid("5c198568-5e41-46aa-9a4b-1565c66100e7")
            },
        };

        /// <summary>
        /// Страны
        /// </summary>
        public static readonly Country[] Countries = new[]
        {
            new Country()
            {
                Id = new Guid("bfea0cb8-e4d2-4e7e-a119-caac5b30fa6d"),
                Name = "Name_1",
                Continent = "Continent_1"
            }
        };

        /// <summary>
        /// Города
        /// </summary>
        public static readonly City[] Cities = new[]
        {
            new City()
            {
                Id = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567"),
                CountryId = new Guid("bfea0cb8-e4d2-4e7e-a119-caac5b30fa6d"),
                Name = "Name_1"
            }
        };

        /// <summary>
        /// Производители
        /// </summary>
        public static readonly Manufacturer[] Manufacturers = new[]
        {
            new Manufacturer()
            {
                Id = new Guid("b645bb1d-7463-4206-8d30-f2a565f154b6"),
                OrganizationName = "OrganizationName_1",
                StreetAddress = new Address()
                {
                    StreetName = "StreetName_1",
                    PostalCode = "PostalCode_1",
                    CityId = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567")
                }
            }
        };

        /// <summary>
        /// Бренды
        /// </summary>
        public static readonly Brand[] Brands = new[]
        {
            new Brand()
            {
                Id = new Guid("70a5ba68-10f4-47af-b293-21b595b7d477"),
                Name = "Name_1"
            }
        };

        /// <summary>
        /// Категории
        /// </summary>
        public static readonly Category[] Categories = new[]
        {
            new Category()
            {
                Id = new Guid("b0fd6429-3de5-403a-b633-ca3bd928deb1"),
                Name = "Name_1"
            }
        };

        /// <summary>
        /// Поставщики
        /// </summary>
        public static readonly Shipper[] Shippers = new[]
        {
            new Shipper()
            {
                Id = new Guid("6f794d7d-5396-4f8a-8fde-78090e062203"),
                OrganizationName = "OrganizationName_1",
                ContactPhone = "ContactPhone_1",
                HeadOffice = new Address()
                {
                    StreetName = "StreetName_2",
                    PostalCode = "PostalCode_2",
                    CityId = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567")
                }
            }
        };

        /// <summary>
        /// Товары
        /// </summary>
        public static readonly Product[] Products = new[]
        {
            new Product()
            {
                Id = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076"),
                BrandId = new Guid("70a5ba68-10f4-47af-b293-21b595b7d477"),
                CategoryId = new Guid("b0fd6429-3de5-403a-b633-ca3bd928deb1"),
                ManufacturerId = new Guid("b645bb1d-7463-4206-8d30-f2a565f154b6"),
                Name = "Name_1",
                CreateDate = DateTime.UtcNow,
                Description = "Description_1",
                Price = 100,
                ReviewsCount = 1,
                AverageRating = ProductRating.FiveStars
            }
        };

        /// <summary>
        /// Комментарии к товарам
        /// </summary>
        public static readonly ProductComment[] ProductComments = new[]
        {
            new ProductComment()
            {
                Id = new Guid("18c4dc23-3059-49a5-8174-f433123efea7"),
                AuthorId = new Guid("7f46fdda-8ab7-48eb-b713-2970b5038485"),
                ProductId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076"),
                Body = "Body_1",
                CreateDate = DateTime.UtcNow
            },
            new ProductComment()
            {
                Id = new Guid("9a0e002d-7eb4-45a1-a796-11c94456bda6"),
                AuthorId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03"),
                ProductId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076"),
                ParentCommentId = new Guid("18c4dc23-3059-49a5-8174-f433123efea7"),
                Body = "Body_2",
                CreateDate = DateTime.UtcNow
            }
        };

        /// <summary>
        /// Обзоры на товары
        /// </summary>
        public static readonly ProductReview[] ProductReviews = new[]
        {
            new ProductReview()
            {
                Id = new Guid("43de32f1-2378-41ca-8c29-c47dda69c929"),
                AuthorId = new Guid("7f46fdda-8ab7-48eb-b713-2970b5038485"),
                ProductId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076"),
                Comment = "Comment_1",
                Title = "Title_1",
                Rating = ProductRating.FiveStars,
                CreateDate = DateTime.UtcNow
            }
        };
    }
}
