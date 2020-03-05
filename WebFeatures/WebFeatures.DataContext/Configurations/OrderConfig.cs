using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Model.OrderAggregate;

namespace WebFeatures.DataContext.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress);
            builder.OwnsOne(o => o.BillingAddress);
        }
    }
}
