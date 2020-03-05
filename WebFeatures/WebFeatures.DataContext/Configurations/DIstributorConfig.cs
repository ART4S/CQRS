using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class DistributorConfig : IEntityTypeConfiguration<Distributor>
    {
        public void Configure(EntityTypeBuilder<Distributor> builder)
        {
            builder.OwnsMany(d => d.ShippingCenters);
        }
    }
}
