﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}