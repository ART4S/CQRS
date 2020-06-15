using Moq;
using Shouldly;
using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Common.SystemTime;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.TestObjects;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    public class WriteRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_SetsCurrentDateToCreateDateProperty()
        {
            // Arrange
            var now = DateTime.UtcNow;

            var datetime = new Mock<IDateTime>();
            datetime.Setup(x => x.Now).Returns(() => now);

            DateTimeProvider.DateTime = datetime.Object;

            var connection = new Mock<IDbConnection>();

            var entityMap = new Mock<IEntityMap<TestEntity>>();
            var profile = new Mock<IEntityProfile>();

            profile.Setup(x => x.GetMap<TestEntity>()).Returns(() => entityMap.Object);

            var repo = new WriteRepository<TestEntity>(connection.Object, profile.Object);
            var entity = new TestEntity();

            // Act
            await repo.CreateAsync(entity);

            // Assert
            entity.CreateDate.ShouldBe(now);
        }

        [Fact]
        public async Task CreateAsync_GeneretesNewId_WhenUserPassedWithoutId()
        {
            // Arrange
            var connection = new Mock<IDbConnection>();
            var profile = new Mock<IEntityProfile>();
            var repo = new WriteRepository<TestEntity>(connection.Object, profile.Object);

            var entity = new TestEntity();

            // Act
            await repo.CreateAsync(entity);

            // Assert
            entity.Id.ShouldNotBe(default);
        }
    }
}