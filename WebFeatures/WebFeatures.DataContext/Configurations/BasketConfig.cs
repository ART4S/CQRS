using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Entities;

namespace WebFeatures.DataContext.Configurations
{
    public class BasketConfig : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.BasketItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(x => x.User)
                .WithOne().IsRequired(false)
                .HasForeignKey<Basket>(x => x.Id).IsRequired();
        }
    }
}
