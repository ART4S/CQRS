using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    public class ManufacturerRepositoryTests : IntegrationTestBase
    {
        public ManufacturerRepositoryTests(DatabaseFixture database) : base(database)
        {
        }

        private ManufacturerWriteRepository CreateDefaultRepository()
        {
            var profile = new EntityProfile();

            profile.TryRegisterMap(typeof(ManufacturerMap));

            return new ManufacturerWriteRepository(Database.Connection, new DapperDbExecutor(), profile);
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
        public async Task GetByIdAsync_WhenManufacturerExists_ReturnsManufacturer()
        {
            // Arrange
            ManufacturerWriteRepository repo = CreateDefaultRepository();

            Guid manufacturerId = new Guid("278a79e9-5889-4953-a7c9-448c1e185600");
            Guid cityId = new Guid("b27a7a05-d61f-4559-9b04-5fd282a694d3");

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
        public async Task GetByIdAsync_WhenManufacturerDoesntExist_ReturnsNull()
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
                    CityId = new Guid("b27a7a05-d61f-4559-9b04-5fd282a694d3"),
                    PostalCode = "",
                    StreetName = ""
                }
            };

            // Act
            await repo.CreateAsync(manufacturer);

            int manufacturersCount = await Database.Connection.ExecuteScalarAsync<int>(
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