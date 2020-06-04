using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.Tests.Helpers.Utils
{
    internal static class SqlBuilder
    {
        public static SqlQuery DropDatabase(string databaseName)
        {
            string query = $"DROP DATABASE IF EXISTS {databaseName}";

            return new SqlQuery(query);
        }

        public static SqlQuery CreateDatabase(string databaseName)
        {
            string query = $"CREATE DATABASE {databaseName}";

            return new SqlQuery(query);
        }

        public static SqlQuery CloseExistingConnections(string databaseName)
        {
            string query =
                @"SELECT pg_terminate_backend(pid) 
                FROM pg_stat_activity
                WHERE datname = @databaseName AND pid <> pg_backend_pid()";

            return new SqlQuery(query, new { databaseName });
        }

        public static SqlQuery CreateSchema()
        {
            string createTables =
                $"{System.IO.File.ReadAllText("Scripts/Schema/Tables.sql")};\n";

            string createFunctions =
                $"{System.IO.File.ReadAllText("Scripts/Schema/Functions.sql")};\n";

            return new SqlQuery(createTables + createFunctions);
        }

        public static SqlQuery InsertUser(User user)
        {
            string query =
                @$"INSERT INTO public.Users 
                (Id, Name, Email, PasswordHash, PictureId) 
                VALUES (@Id, @Name, @Email, @PasswordHash, @PictureId)";

            return new SqlQuery(query, user);
        }

        public static SqlQuery InsertRole(Role role)
        {
            string query =
                @$"INSERT INTO public.Roles 
                (Id, Name, Description) 
                VALUES (@Id, @Name, @Description)";

            return new SqlQuery(query, role);
        }

        public static SqlQuery InsertUserRole(UserRole userRole)
        {
            string query =
                @$"INSERT INTO public.UserRoles 
                (UserId, RoleId) 
                VALUES (@UserId, @RoleId)";

            return new SqlQuery(query, userRole);
        }

        public static SqlQuery InsertCountry(Country country)
        {
            string query =
                @$"INSERT INTO public.Countries 
                (Id, Name, Continent) 
                VALUES (@Id, @Name, @Continent)";

            return new SqlQuery(query, country);
        }

        public static SqlQuery InsertCity(City city)
        {
            string query =
                @$"INSERT INTO public.Cities 
                (Id, Name, CountryId) 
                VALUES (@Id, @Name, @CountryId)";

            return new SqlQuery(query, city);
        }

        public static SqlQuery InsertBrand(Brand brand)
        {
            string query =
                @$"INSERT INTO public.Brands 
                (Id, Name) 
                VALUES (@Id, @Name)";

            return new SqlQuery(query, brand);
        }

        public static SqlQuery InsertCategory(Category category)
        {
            string query =
                @$"INSERT INTO public.Categories 
                (Id, Name) 
                VALUES (@Id, @Name)";

            return new SqlQuery(query, category);
        }

        public static SqlQuery InsertManufacturer(Manufacturer manufacturer)
        {
            string query =
                @$"INSERT INTO public.Manufacturers 
                (Id, OrganizationName, StreetAddress_StreetName, StreetAddress_PostalCode, StreetAddress_CityId) 
                VALUES (@Id, @OrganizationName, @StreetAddress_StreetName, @StreetAddress_PostalCode, @StreetAddress_CityId)";

            var param = new
            {
                manufacturer.Id,
                manufacturer.OrganizationName,
                StreetAddress_StreetName = manufacturer.StreetAddress.StreetName,
                StreetAddress_PostalCode = manufacturer.StreetAddress.PostalCode,
                StreetAddress_CityId = manufacturer.StreetAddress.CityId,
            };

            return new SqlQuery(query, param);
        }

        public static SqlQuery InsertShipper(Shipper shipper)
        {
            string query =
                @$"INSERT INTO public.Shippers 
                (Id, OrganizationName, ContactPhone, HeadOffice_StreetName, HeadOffice_PostalCode, HeadOffice_CityId) 
                VALUES (@Id, @OrganizationName, @ContactPhone, @HeadOffice_StreetName, @HeadOffice_PostalCode, @HeadOffice_CityId)";

            var param = new
            {
                shipper.Id,
                shipper.OrganizationName,
                shipper.ContactPhone,
                HeadOffice_StreetName = shipper.HeadOffice.StreetName,
                HeadOffice_PostalCode = shipper.HeadOffice.PostalCode,
                HeadOffice_CityId = shipper.HeadOffice.CityId,
            };

            return new SqlQuery(query, param);
        }

        public static SqlQuery InsertProduct(Product product)
        {
            string query =
                @$"INSERT INTO public.Products 
                (Id, Name, Price, Description, CreateDate, PictureId, ManufacturerId, CategoryId, BrandId) 
                VALUES (@Id, @Name, @Price, @Description, @CreateDate, @PictureId, @ManufacturerId, @CategoryId, @BrandId)";

            return new SqlQuery(query, product);
        }

        public static SqlQuery InsertProductReview(ProductReview review)
        {
            string query =
                @$"INSERT INTO public.ProductReviews 
                (Id, Title, Comment, CreateDate, Rating, AuthorId, ProductId) 
                VALUES (@Id, @Title, @Comment, @CreateDate, @Rating, @AuthorId, @ProductId)";

            return new SqlQuery(query.ToString(), review);
        }

        public static SqlQuery InsertProductComment(ProductComment comment)
        {
            string query =
                @$"INSERT INTO public.ProductComments 
                (Id, Body, CreateDate, ProductId, AuthorId, ParentCommentId) 
                VALUES (@Id, @Body, @CreateDate, @ProductId, @AuthorId, @ParentCommentId) ";

            return new SqlQuery(query, comment);
        }
    }
}