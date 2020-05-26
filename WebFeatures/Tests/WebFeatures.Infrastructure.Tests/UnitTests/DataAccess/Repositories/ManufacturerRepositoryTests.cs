using Dapper;
using Shouldly;
using System;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.DataAccess.Repositories
{
    [Collection("PostgreSqlDatabase")]
    public class ManufacturerRepositoryTests
    {
        private readonly PostgreSqlDatabaseFixture _db;
        private readonly Repository<Manufacturer, QueryBuilder<Manufacturer>> _repo;

        public ManufacturerRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _db = db;
            _repo = new Repository<Manufacturer, QueryBuilder<Manufacturer>>(
                db.Connection,
                new QueryBuilder<Manufacturer>(
                    new EntityProfile()));
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateOneManufacturer()
        {
            // Arrange
            var manufacturer = new Manufacturer()
            {
                Id = new Guid("0fd1e2cd-51a0-4ba5-b830-e0b7cbe76823"),
                OrganizationName = "",
                HomePageUrl = "",
                StreetAddress = new Address()
                {
                    CityId = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567"),
                    PostalCode = "",
                    StreetName = ""
                }
            };

            // Act
            await _repo.CreateAsync(manufacturer);

            int manufacturersCount = await _db.Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM public.Manufacturers
                WHERE Id = @Id AND StreetAddress_CityId = @CityId",
                new
                {
                    manufacturer.Id,
                    manufacturer.StreetAddress.CityId
                });

            // Assert
            manufacturersCount.ShouldBe(1);
        }
    }
}
