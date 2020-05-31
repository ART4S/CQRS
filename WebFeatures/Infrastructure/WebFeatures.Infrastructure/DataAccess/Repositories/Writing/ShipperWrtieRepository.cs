using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class ShipperWrtieRepository : WriteRepository<Shipper, IQueryBuilder<Shipper>>
    {
        public ShipperWrtieRepository(
            IDbConnection connection,
            IQueryBuilder<Shipper> queryBuilder) : base(connection, queryBuilder)
        {
        }

        public override async Task<IEnumerable<Shipper>> GetAllAsync()
        {
            SqlQuery sql = QueryBuilder.BuildGetAll();

            IEnumerable<Shipper> shipper =
                await Connection.QueryAsync<Shipper, Address, Shipper>(
                    sql.Query,
                    (shipper, address) =>
                    {
                        shipper.HeadOffice = address;
                        return shipper;
                    },
                    param: sql.Param,
                    splitOn: sql.SplitOn);

            return shipper;
        }

        public override async Task<Shipper> GetAsync(Guid id)
        {
            SqlQuery sql = QueryBuilder.BuildGet(id);

            Shipper shipper =
                (await Connection.QueryAsync<Shipper, Address, Shipper>(
                    sql.Query,
                    (shipper, address) =>
                    {
                        shipper.HeadOffice = address;
                        return shipper;
                    },
                    param: sql.Param,
                    splitOn: sql.SplitOn))
                .FirstOrDefault();

            return shipper;
        }
    }
}
