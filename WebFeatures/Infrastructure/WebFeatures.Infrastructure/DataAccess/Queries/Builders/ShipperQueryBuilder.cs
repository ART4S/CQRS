using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class ShipperQueryBuilder : QueryBuilder<Shipper>, IQueryBuilder<Shipper>
    {
        public ShipperQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        public override SqlQuery BuildGetAll()
        {
            IEntityMap<Shipper> entityMap = Profile.GetMap<Shipper>();

            string query = string.Format(
                $@"SELECT 
                    {entityMap.Column(x => x.Id)}, 
                    {entityMap.Column(x => x.OrganizationName)}, 
                    {entityMap.Column(x => x.ContactPhone)},
                    {entityMap.Column(x => x.HeadOffice.CityId)} as {nameof(Address.CityId)}, 
                    {entityMap.Column(x => x.HeadOffice.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {entityMap.Column(x => x.HeadOffice.StreetName)} as {nameof(Address.StreetName)} 
                FROM {entityMap.Table.NameWithSchema()}");

            string splitOn = string.Join(", ",
                entityMap.Column(x => x.Id),
                nameof(Address.CityId));

            return new SqlQuery(query, splitOn: splitOn);
        }

        public override SqlQuery BuildGet(Guid id)
        {
            IEntityMap<Shipper> entityMap = Profile.GetMap<Shipper>();

            string query = string.Format(
                $@"SELECT 
                    {entityMap.Column(x => x.Id)}, 
                    {entityMap.Column(x => x.OrganizationName)}, 
                    {entityMap.Column(x => x.ContactPhone)},
                    {entityMap.Column(x => x.HeadOffice.CityId)} as {nameof(Address.CityId)}, 
                    {entityMap.Column(x => x.HeadOffice.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {entityMap.Column(x => x.HeadOffice.StreetName)} as {nameof(Address.StreetName)} 
                FROM {entityMap.Table.NameWithSchema()} 
                WHERE {entityMap.Column(x => x.Id)} = @id");

            string splitOn = string.Join(", ",
                entityMap.Column(x => x.Id),
                nameof(Address.CityId));

            return new SqlQuery(query, new { id }, splitOn);
        }
    }
}