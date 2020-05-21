using System;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.DataAccess
{
    public class ProductCommentRepositoryTests : IClassFixture<NpgsqlDatabaseFixture>
    {
        private readonly NpgsqlDatabaseFixture _db;
        private readonly ProductCommentRepository _repo;

        public ProductCommentRepositoryTests(NpgsqlDatabaseFixture db)
        {
            _db = db;
            _repo = new ProductCommentRepository(db.Connection, new ProductCommentQueryBuilder(new EntityProfile()));
        }

        [Fact]
        public void GetByProductAsync_ShouldReturnRecordsWhenProductExists()
        {
            Guid existingProductId = new Guid("8204d13e-32eb-456c-8161-369c7c86d504");
        }

        [Fact]
        public void GetByProductAsync_ShouldReturnEmptyCollectionWhenProductDoesntExists()
        {
            Guid nonExistingProductId = Guid.NewGuid();
        }
    }
}
