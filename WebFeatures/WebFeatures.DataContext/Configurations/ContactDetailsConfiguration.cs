using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.DataContext.Configurations
{
    public class ContactDetailsConfiguration : IEntityTypeConfiguration<ContactDetails>
    {
        public void Configure(EntityTypeBuilder<ContactDetails> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(x => x.ContactDetails)
                .HasForeignKey<ContactDetails>(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(x => x.City).IsRequired();
                a.Property(x => x.Country).IsRequired();
                a.Property(x => x.State).IsRequired();
                a.Property(x => x.Street).IsRequired();
                a.Property(x => x.ZipCode).IsRequired();
            });
        }
    }
}
