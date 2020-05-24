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
    public class ManufacturerRepositoryTests : IClassFixture<NpgsqlDatabaseFixture>
    {
        private readonly NpgsqlDatabaseFixture _db;
        private readonly Repository<Manufacturer, QueryBuilder<Manufacturer>> _repo;

        public ManufacturerRepositoryTests(NpgsqlDatabaseFixture db)
        {
            _db = db;
            _repo = new Repository<Manufacturer, QueryBuilder<Manufacturer>>(
                db.Connection,
                new QueryBuilder<Manufacturer>(
                    new EntityProfile()));
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateRecord()
        {
            // Arrange
            Guid manufacturerId = new Guid("0fd1e2cd-51a0-4ba5-b830-e0b7cbe76823");
            Guid cityId = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567");

            var manufacturer = new Manufacturer()
            {
                Id = manufacturerId,
                OrganizationName = "",
                HomePageUrl = "",
                StreetAddress = new Address()
                {
                    CityId = cityId,
                    PostalCode = "",
                    StreetName = ""
                }
            };

            // Act
            await _repo.CreateAsync(manufacturer);

            int manufacturersCount = await _db.Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM public.Manufacturers
                WHERE Id = @manufacturerId AND StreetAddress_CityId = @cityId",
                new
                {
                    manufacturerId,
                    cityId
                });

            // Assert
            manufacturer.Id.ShouldBe(manufacturerId);
            manufacturer.StreetAddress.CityId.ShouldBe(cityId);
            manufacturersCount.ShouldBe(1);
        }
    }
}
