using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebFeatures.Domian.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            throw new NotImplementedException();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(to => to, from => DateTime.SpecifyKind(from, DateTimeKind.Utc));

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasConversion(to => to, 
                    from => from.HasValue 
                        ? DateTime.SpecifyKind(from.Value, DateTimeKind.Utc) 
                        : from);
        }
    }
}
