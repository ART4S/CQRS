using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class BasketConfig : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne()
                .IsRequired(false)
                .HasForeignKey<Basket>(x => x.UserId)
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Basket.BasketItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany(
                x => x.BasketItems,
                x =>
                {
                    x.WithOwner().HasForeignKey("OwnerId");
                    x.Property<Guid>("Id");
                    x.HasKey("Id");

                    x.HasOne(y => y.Product)
                        .WithMany()
                        .HasForeignKey(y => y.ProductId)
                        .IsRequired(false);
                });
        }
    }
}
