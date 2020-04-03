using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Entities;

namespace WebFeatures.WriteContext.Configurations
{
    public class ManufacturerConfig : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.Property(x => x.OrganizationName).IsRequired();
            builder.OwnsOne(x => x.StreetAddress, x =>
            {
                x.WithOwner();
                x.HasOne(y => y.City);
            });
        }
    }
}
