using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
