using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class ManufacturerWriteRepository : WriteRepository<Manufacturer>
    {
        public ManufacturerWriteRepository(IDbConnection connection, IEntityProfile profile) : base(connection, profile)
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
                await Connection.QueryAsync<Manufacturer, Address, Manufacturer>(
                    sql,
                    (manufacturer, address) =>
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
                (await Connection.QueryAsync<Manufacturer, Address, Manufacturer>(
                    sql,
                    (manufacturer, address) =>
                    {
                        manufacturer.StreetAddress = address;
                        return manufacturer;
                    },
                    param: new { id },
                    splitOn: nameof(Address.CityId)))
                .FirstOrDefault();

            return manufacturer;
        }
    }
}