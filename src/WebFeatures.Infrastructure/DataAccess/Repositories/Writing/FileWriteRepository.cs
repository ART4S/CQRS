using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Common.Extensions;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(
            IDbConnection connection,
            IDbExecutor executor,
            IEntityProfile profile) : base(connection, executor, profile)
        {
        }

        public Task<IEnumerable<File>> GetByProductIdAsync(Guid productId)
        {
            IEntityMap<File> file = Entity;
            IEntityMap<ProductPicture> productPicture = Profile.GetMap<ProductPicture>();

            string sql =
                @$"SELECT 
                    {file.Properties.Select(x => $"f.{x.ColumnName}").JoinString()} 
                FROM 
                    {file.Table.NameWithSchema()} f 
                JOIN 
                    {productPicture.Table.NameWithSchema()} p ON p.{productPicture.Column(x => x.FileId)} = f.{file.Column(x => x.Id)} 
                WHERE 
                    p.{productPicture.Column(x => x.ProductId)} = @productId";

            return Executor.QueryAsync<File>(Connection, sql, new { productId });
        }
    }
}