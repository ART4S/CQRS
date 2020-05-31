using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.IntegrationalTests.Repositories.Writing
{
    [Collection("PostgreSqlDatabase")]
    public class ManufacturerRepositoryTests
    {
        private readonly PostgreSqlDatabaseFixture _db;
        private readonly ManufacturerWriteRepository _repo;

        public ManufacturerRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _db = db;

            var profile = new EntityProfile();
            profile.AddMappingsFromAssembly(typeof(ManufacturerMap).Assembly);

            _repo = new ManufacturerWriteRepository(db.Connection, new ManufacturerQueryBuilder(profile));
        }

        [Fact]
        public async Task GetAllAsync_ShouldNotReturnEmptyCollection()
        {
            // Act
            IEnumerable<Manufacturer> manufacturers = await _repo.GetAllAsync();

            // Assert
            manufacturers.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnExistingManufacturer()
        {
            // Arrange
            Guid existingManufacturerId = new Guid("b645bb1d-7463-4206-8d30-f2a565f154b6");
            Guid existingManufacturerCityId = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567");

            // Act
            Manufacturer manufacturer = await _repo.GetAsync(existingManufacturerId);

            // Assert
            manufacturer.ShouldNotBeNull();
            manufacturer.Id.ShouldBe(existingManufacturerId);
            manufacturer.StreetAddress.ShouldNotBeNull();
            manufacturer.StreetAddress.CityId.ShouldBe(existingManufacturerCityId);
            manufacturer.StreetAddress.StreetName.ShouldNotBeNull();
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
