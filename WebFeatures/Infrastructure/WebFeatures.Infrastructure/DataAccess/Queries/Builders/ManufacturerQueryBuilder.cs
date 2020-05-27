using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class ManufacturerQueryBuilder : QueryBuilder<Manufacturer>
    {
        public ManufacturerQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        public override SqlQuery BuildGetAll()
        {
            IEntityMap<Manufacturer> entityMap = Profile.GetMap<Manufacturer>();

            string query = string.Format(
                $@"SELECT 
                    {entityMap.Column(x => x.Id)}, 
                    {entityMap.Column(x => x.OrganizationName)}, 
                    {entityMap.Column(x => x.StreetAddress.CityId)} as {nameof(Address.CityId)}, 
                    {entityMap.Column(x => x.StreetAddress.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {entityMap.Column(x => x.StreetAddress.StreetName)} as {nameof(Address.StreetName)} 
                FROM Manufacturers");

            string splitOn = string.Join(", ",
                entityMap.Column(x => x.Id),
                nameof(Address.CityId));

            return new SqlQuery(query, splitOn: splitOn);
        }

        public override SqlQuery BuildGet(Guid id)
        {
            IEntityMap<Manufacturer> entityMap = Profile.GetMap<Manufacturer>();

            string query = string.Format(
                $@"SELECT 
                    {entityMap.Column(x => x.Id)}, 
                    {entityMap.Column(x => x.OrganizationName)}, 
                    {entityMap.Column(x => x.StreetAddress.CityId)} as {nameof(Address.CityId)}, 
                    {entityMap.Column(x => x.StreetAddress.PostalCode)} as {nameof(Address.PostalCode)}, 
                    {entityMap.Column(x => x.StreetAddress.StreetName)} as {nameof(Address.StreetName)} 
                FROM Manufacturers
                WHERE {entityMap.Column(x => x.Id)} = @id");

            string splitOn = string.Join(", ",
                entityMap.Column(x => x.Id),
                nameof(Address.CityId));

            return new SqlQuery(query, new { id }, splitOn);
        }
    }
}
