using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class ManufacturerConfig : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.OwnsOne(x => x.StreetAddress, x =>
            {
                x.WithOwner();
                x.HasOne(y => y.City);
            });
        }
    }
}
