using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class ManufacturerConfig : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
