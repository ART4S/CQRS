using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings
{
	internal class ShipperMap : EntityMap<Shipper>
	{
		public ShipperMap()
		{
			MapProperty(x => x.HeadOffice.CityId).ToColumn("HeadOffice_CityId");
			MapProperty(x => x.HeadOffice.StreetName).ToColumn("HeadOffice_StreetName");
			MapProperty(x => x.HeadOffice.PostalCode).ToColumn("HeadOffice_PostalCode");
		}
	}
}
