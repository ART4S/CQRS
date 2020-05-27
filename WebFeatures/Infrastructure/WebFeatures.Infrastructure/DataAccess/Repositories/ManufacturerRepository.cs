using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class ManufacturerRepository : Repository<Manufacturer, IQueryBuilder<Manufacturer>>, IAsyncRepository<Manufacturer>
    {
        public ManufacturerRepository(
            IDbConnection connection,
            IQueryBuilder<Manufacturer> queryBuilder) : base(connection, queryBuilder)
        {
        }

        public override async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            SqlQuery sql = QueryBuilder.BuildGetAll();

            IEnumerable<Manufacturer> manufacturers =
                await Connection.QueryAsync<Manufacturer, Address, Manufacturer>(
                    sql.Query,
                    (manufacturer, address) =>
                    {
                        manufacturer.StreetAddress = address;
                        return manufacturer;
                    },
                    param: sql.Param,
                    splitOn: sql.SplitOn);

            return manufacturers;
        }

        public override async Task<Manufacturer> GetAsync(Guid id)
        {
            SqlQuery sql = QueryBuilder.BuildGet(id);

            Manufacturer manufacturer =
                (await Connection.QueryAsync<Manufacturer, Address, Manufacturer>(
                    sql.Query,
                    (manufacturer, address) =>
                    {
                        manufacturer.StreetAddress = address;
                        return manufacturer;
                    },
                    param: sql.Param,
                    splitOn: sql.SplitOn))
                .FirstOrDefault();

            return manufacturer;
        }
    }
}