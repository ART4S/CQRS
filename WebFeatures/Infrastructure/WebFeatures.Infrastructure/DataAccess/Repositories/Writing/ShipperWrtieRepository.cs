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
    internal class ShipperWrtieRepository : WriteRepository<Shipper>
    {
        public ShipperWrtieRepository(IDbConnection connection, IEntityProfile profile) : base(connection, profile)
        {
        }

        public override async Task<IEnumerable<Shipper>> GetAllAsync()
        {
            string sql =
                $@"SELECT 
                    {Entity.Column(x => x.Id)}, 
                    {Entity.Column(x => x.OrganizationName)}, 
                    {Entity.Column(x => x.ContactPhone)},
                    {Entity.Column(x => x.HeadOffice.CityId)} as {nameof(Address.CityId)}, 
                    {Entity.Column(x => x.HeadOffice.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {Entity.Column(x => x.HeadOffice.StreetName)} as {nameof(Address.StreetName)} 
                FROM 
                    {Entity.Table.NameWithSchema()}";

            IEnumerable<Shipper> shipper =
                await Connection.QueryAsync<Shipper, Address, Shipper>(
                    sql,
                    (shipper, address) =>
                    {
                        shipper.HeadOffice = address;
                        return shipper;
                    },
                    splitOn: nameof(Address.CityId));

            return shipper;
        }

        public override async Task<Shipper> GetAsync(Guid id)
        {
            string sql =
                $@"SELECT 
                    {Entity.Column(x => x.Id)}, 
                    {Entity.Column(x => x.OrganizationName)}, 
                    {Entity.Column(x => x.ContactPhone)},
                    {Entity.Column(x => x.HeadOffice.CityId)} as {nameof(Address.CityId)}, 
                    {Entity.Column(x => x.HeadOffice.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {Entity.Column(x => x.HeadOffice.StreetName)} as {nameof(Address.StreetName)} 
                FROM 
                    {Entity.Table.NameWithSchema()} 
                WHERE 
                    {Entity.Column(x => x.Id)} = @id";

            Shipper shipper =
                (await Connection.QueryAsync<Shipper, Address, Shipper>(
                    sql,
                    (shipper, address) =>
                    {
                        shipper.HeadOffice = address;
                        return shipper;
                    },
                    splitOn: nameof(Address.CityId)))
                .FirstOrDefault();

            return shipper;
        }
    }
}