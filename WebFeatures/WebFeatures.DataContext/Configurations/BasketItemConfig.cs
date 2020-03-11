using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class BasketItemConfig : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasOne(x => x.Product)
                .WithOne().IsRequired(false)
                .HasForeignKey<BasketItem>(x => x.ProductId).IsRequired();
        }
    }
}
