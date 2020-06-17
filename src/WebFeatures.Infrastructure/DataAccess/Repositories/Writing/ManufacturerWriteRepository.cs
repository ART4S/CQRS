using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class ManufacturerWriteRepository : WriteRepository<Manufacturer>
    {
        public ManufacturerWriteRepository(
            IDbConnection connection,
            IDbExecutor executor,
            IEntityProfile profile) : base(connection, executor, profile)
        {
        }

        public override async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            string sql =
                $@"SELECT 
                    {Entity.Column(x => x.Id)}, 
                    {Entity.Column(x => x.OrganizationName)}, 
                    {Entity.Column(x => x.StreetAddress.CityId)} as {nameof(Address.CityId)}, 
                    {Entity.Column(x => x.StreetAddress.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {Entity.Column(x => x.StreetAddress.StreetName)} as {nameof(Address.StreetName)} 
                FROM 
                    {Entity.Table.NameWithSchema()}";

            IEnumerable<Manufacturer> manufacturers =
                await Executor.QueryAsync<Manufacturer, Address, Manufacturer>(
                    Connection,
                    sql,
                    map: (manufacturer, address) =>
                    {
                        manufacturer.StreetAddress = address;
                        return manufacturer;
                    },
                    splitOn: nameof(Address.CityId));

            return manufacturers;
        }

        public override async Task<Manufacturer> GetAsync(Guid id)
        {
            string sql =
                $@"SELECT 
                    {Entity.Column(x => x.Id)}, 
                    {Entity.Column(x => x.OrganizationName)}, 
                    {Entity.Column(x => x.StreetAddress.CityId)} as {nameof(Address.CityId)}, 
                    {Entity.Column(x => x.StreetAddress.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {Entity.Column(x => x.StreetAddress.StreetName)} as {nameof(Address.StreetName)} 
                FROM 
                    {Entity.Table.NameWithSchema()} 
                WHERE 
                    {Entity.Column(x => x.Id)} = @id";

            Manufacturer manufacturer =
                (await Executor.QueryAsync<Manufacturer, Address, Manufacturer>(
                    Connection,
                    sql,
                    param: new { id },
                    map: (manufacturer, address) =>
                    {
                        manufacturer.StreetAddress = address;
                        return manufacturer;
                    },
                    splitOn: nameof(Address.CityId)))
                .FirstOrDefault();

            return manufacturer;
        }
    }
}