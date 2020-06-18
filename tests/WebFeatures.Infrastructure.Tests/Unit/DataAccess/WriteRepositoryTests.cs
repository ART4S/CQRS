using Moq;
using Shouldly;
using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Common.SystemTime;
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

        private WriteRepository<TestEntity> CreateDefaultRepository()
        {
            _profile.Setup(x => x.GetMap<TestEntity>()).Returns(new TestEntityMap());

            return new WriteRepository<TestEntity>(_connection.Object, _executor.Object, _profile.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityHasCreateDateProperty_SetsCurrentDate()
        {
            // Arrange
            var now = DateTime.UtcNow;

            var datetime = new Mock<IDateTime>();

            datetime.Setup(x => x.Now).Returns(now);

            DateTimeProvider.DateTime = datetime.Object;

            WriteRepository<TestEntity> repo = CreateDefaultRepository();
            TestEntity entity = new TestEntity();

            // Act
            await repo.CreateAsync(entity);

            // Assert
            entity.CreateDate.ShouldBe(now);
        }

        [Fact]
        public async Task CreateAsync_WhenEntityPassedWithoutId_SetsNewId()
        {
            // Arrange
            WriteRepository<TestEntity> repo = CreateDefaultRepository();
            TestEntity entity = new TestEntity();

            // Act
            await repo.CreateAsync(entity);

            // Assert
            entity.Id.ShouldNotBe(default);
        }

        [Fact]
        public async Task DeleteAsync_WhenEmptyCollection_DoesntCallExecutor()
        {
            // Arrange
            var repo = CreateDefaultRepository();

            // Act
            await repo.DeleteAsync(new TestEntity[0]);

            // Assert
            _executor.Verify(x => x.ExecuteAsync(_connection.Object, It.IsAny<string>()), Times.Never);
        }
    }
}