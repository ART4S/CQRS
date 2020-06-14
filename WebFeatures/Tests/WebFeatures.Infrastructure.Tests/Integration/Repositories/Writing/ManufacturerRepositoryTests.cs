using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    [Collection("PostgreSqlDatabase")]
    public class ManufacturerRepositoryTests
    {
        private readonly IDbConnection _connection;

        public ManufacturerRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _connection = db.Connection;
        }

        private ManufacturerWriteRepository CreateDefaultRepository()
        {
            var profile = new EntityProfile();

            profile.TryRegisterMap(typeof(ManufacturerMap));

            return new ManufacturerWriteRepository(_connection, profile);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNonEmptyCollection()
        {
            // Arrange
            ManufacturerWriteRepository repo = CreateDefaultRepository();

            // Act
            IEnumerable<Manufacturer> manufacturers = await repo.GetAllAsync();

            // Assert
            manufacturers.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsManufacturer_IfManufacturerExists()
        {
            // Arrange
            ManufacturerWriteRepository repo = CreateDefaultRepository();

            Guid manufacturerId = new Guid("b645bb1d-7463-4206-8d30-f2a565f154b6");
            Guid cityId = new Guid("f2c32c06-c7be-4a5e-ba96-41b0d9b9b567");

            // Act
            Manufacturer manufacturer = await repo.GetAsync(manufacturerId);

            // Assert
            manufacturer.ShouldNotBeNull();
            manufacturer.Id.ShouldBe(manufacturerId);
            manufacturer.StreetAddress.ShouldNotBeNull();
            manufacturer.StreetAddress.CityId.ShouldBe(cityId);
            manufacturer.StreetAddress.StreetName.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_IfManufacturerDoesntExist()
        {
            // Arrange
            ManufacturerWriteRepository repo = CreateDefaultRepository();
            Guid manufacturerId = Guid.NewGuid();

            // Act
            Manufacturer manufacturer = await repo.GetAsync(manufacturerId);

            // Assert
            manufacturer.ShouldBeNull();
        }

        [Fact]
        public async Task CreateAsync_CreatesOneManufacturer()
        {
            // Arrange
            ManufacturerWriteRepository repo = CreateDefaultRepository();

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
            await repo.CreateAsync(manufacturer);

            int manufacturersCount = await _connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM public.manufacturers
                WHERE id = @Id AND streetaddress_cityid = @CityId",
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
