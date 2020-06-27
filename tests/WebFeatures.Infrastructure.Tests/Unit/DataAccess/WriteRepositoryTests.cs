using FluentAssertions;
using Moq;
using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.DataAccess
{
    public class WriteRepositoryTests
    {
        private readonly Mock<IDbConnection> _connection;
        private readonly Mock<IDbExecutor> _executor;
        private readonly Mock<IEntityProfile> _profile;

        public WriteRepositoryTests()
        {
            _connection = new Mock<IDbConnection>();
            _executor = new Mock<IDbExecutor>();
            _profile = new Mock<IEntityProfile>();
        }

        private WriteRepository<CustomEntity> CreateDefaultRepository()
        {
            _profile.Setup(x => x.GetMap<CustomEntity>()).Returns(new CustomEntityMap());

            return new WriteRepository<CustomEntity>(_connection.Object, _executor.Object, _profile.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityHasCreateDateProperty_SetsCurrentDate()
        {
            // Arrange
            var sysTime = new Mock<SystemTime.ISystemTime>();

            var now = DateTime.UtcNow;

            sysTime.Setup(x => x.Now).Returns(now);

            SystemTime.Instance = sysTime.Object;

            WriteRepository<CustomEntity> sut = CreateDefaultRepository();

            CustomEntity entity = new CustomEntity();

            // Act
            await sut.CreateAsync(entity);

            // Assert
            entity.CreateDate.Should().Be(now);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityPassedWithoutId_SetsNewId()
        {
            // Arrange
            WriteRepository<CustomEntity> sut = CreateDefaultRepository();

            CustomEntity entity = new CustomEntity();

            // Act
            await sut.CreateAsync(entity);

            // Assert
            entity.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeleteAsync_WhenEmptyCollection_DoesntCallExecutor()
        {
            // Arrange
            WriteRepository<CustomEntity> sut = CreateDefaultRepository();

            // Act
            await sut.DeleteAsync(new CustomEntity[0]);

            // Assert
            _executor.Verify(x => x.ExecuteAsync(_connection.Object, It.IsAny<string>()), Times.Never);
        }
    }
}