using Dapper;
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

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(IDbConnection connection, IEntityProfile profile) : base(connection, profile)
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

            return Connection.QueryAsync<File>(sql, new { productId });
        }
    }
}