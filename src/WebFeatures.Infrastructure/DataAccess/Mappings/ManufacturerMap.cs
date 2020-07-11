using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings
{
	internal class ManufacturerMap : EntityMap<Manufacturer>
	{
		public ManufacturerMap()
		{
			MapProperty(x => x.StreetAddress.CityId).ToColumn("StreetAddress_CityId");
			MapProperty(x => x.StreetAddress.StreetName).ToColumn("StreetAddress_StreetName");
			MapProperty(x => x.StreetAddress.PostalCode).ToColumn("StreetAddress_PostalCode");
		}
	}
}
