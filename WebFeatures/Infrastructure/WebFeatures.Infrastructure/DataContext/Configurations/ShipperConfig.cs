using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Entities;

namespace WebFeatures.DataContext.Configurations
{
    public class ShipperConfig : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.Property(x => x.OrganizationName).IsRequired();
            builder.OwnsOne(x => x.HeadOffice, x =>
            {
                x.WithOwner();
                x.HasOne(y => y.City);
            });
        }
    }
}
