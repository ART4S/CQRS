using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Infrastructure.DataAccess.Constants;

namespace WebFeatures.Infrastructure.Tests.Helpers.Utils
{
    // TODO: BulkInsert
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
            var sb = new StringBuilder();

            IEnumerable<string> scripts = Directory.GetFiles("Scripts/Schema", "*.sql")
                .Select(x => System.IO.File.ReadAllText(x));

            foreach (string script in scripts)
            {
                sb.AppendLine(script);
            }

            return new SqlQuery(sb.ToString());
        }

        public static SqlQuery RefreshAllViews()
        {
            var sb = new StringBuilder();

            foreach (string view in ViewNames.All)
            {
                sb.AppendLine($"REFRESH MATERIALIZED VIEW {view};");
            }

            return new SqlQuery(sb.ToString());
        }

        public static SqlQuery InsertUser(User user)
        {
            string query =
                @$"INSERT INTO public.users 
                (id, name, email, passwordhash, pictureid) 
                VALUES (@Id, @Name, @Email, @PasswordHash, @PictureId)";

            return new SqlQuery(query, user);
        }

        public static SqlQuery InsertRole(Role role)
        {
            string query =
                @$"INSERT INTO public.roles 
                (id, name, description) 
                VALUES (@Id, @Name, @Description)";

            return new SqlQuery(query, role);
        }

        public static SqlQuery InsertUserRole(UserRole userRole)
        {
            string query =
                @$"INSERT INTO public.userroles 
                (id, userid, roleid) 
                VALUES (@Id, @UserId, @RoleId)";

            return new SqlQuery(query, userRole);
        }

        public static SqlQuery InsertCountry(Country country)
        {
            string query =
                @$"INSERT INTO public.countries 
                (id, name, continent) 
                VALUES (@Id, @Name, @Continent)";

            return new SqlQuery(query, country);
        }

        public static SqlQuery InsertCity(City city)
        {
            string query =
                @$"INSERT INTO public.cities 
                (id, name, countryId) 
                VALUES (@Id, @Name, @CountryId)";

            return new SqlQuery(query, city);
        }

        public static SqlQuery InsertBrand(Brand brand)
        {
            string query =
                @$"INSERT INTO public.brands 
                (id, name) 
                VALUES (@Id, @Name)";

            return new SqlQuery(query, brand);
        }

        public static SqlQuery InsertCategory(Category category)
        {
            string query =
                @$"INSERT INTO public.categories 
                (id, name) 
                VALUES (@Id, @Name)";

            return new SqlQuery(query, category);
        }

        public static SqlQuery InsertManufacturer(Manufacturer manufacturer)
        {
            string query =
                @$"INSERT INTO public.manufacturers 
                (id, organizationname, streetaddress_streetname, streetaddress_postalcode, streetaddress_cityId) 
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
                @$"INSERT INTO public.shippers 
                (id, organizationName, contactPhone, headoffice_streetname, headoffice_postalcode, headoffice_cityId) 
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
                @$"INSERT INTO public.products 
                (id, name, price, description, createDate, mainpictureid, manufacturerid, categoryid, brandid) 
                VALUES (@Id, @Name, @Price, @Description, @CreateDate, @MainPictureId, @ManufacturerId, @CategoryId, @BrandId)";

            return new SqlQuery(query, product);
        }

        public static SqlQuery InsertProductReview(ProductReview review)
        {
            string query =
                @$"INSERT INTO public.productreviews 
                (id, title, comment, createdate, rating, authorid, productid) 
                VALUES (@Id, @Title, @Comment, @CreateDate, @Rating, @AuthorId, @ProductId)";

            return new SqlQuery(query.ToString(), review);
        }

        public static SqlQuery InsertProductComment(ProductComment comment)
        {
            string query =
                @$"INSERT INTO public.productcomments 
                (id, body, createdate, productid, authorid, parentcommentid) 
                VALUES (@Id, @Body, @CreateDate, @ProductId, @AuthorId, @ParentCommentId) ";

            return new SqlQuery(query, comment);
        }
    }
}