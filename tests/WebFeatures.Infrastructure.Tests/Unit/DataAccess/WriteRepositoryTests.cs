using Moq;
using Shouldly;
using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Common.SystemTime;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.TestObjects;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.DataAccess
{
    public class WriteRepositoryTests
    {
        private WriteRepository<TestEntity> CreateDefaultRepository()
        {
            var dbExecutor = new Mock<IDbExecutor>();

            var connection = new Mock<IDbConnection>();
            var entityMap = new Mock<IEntityMap<TestEntity>>();
            var profile = new Mock<IEntityProfile>();

            profile.Setup(x => x.GetMap<TestEntity>()).Returns(() => new TestEntityMap());

            var repo = new WriteRepository<TestEntity>(connection.Object, dbExecutor.Object, profile.Object);

            return repo;
        }

        [Fact]
        public async Task CreateAsync_WhenEntityHasCreateDateProperty_SetsCurrentDate()
        {
            // Arrange
            var now = DateTime.UtcNow;

            var datetime = new Mock<IDateTime>();
            datetime.Setup(x => x.Now).Returns(() => now);

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
    }
}