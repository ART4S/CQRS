using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WebFeatures.DataContext.Configurations
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRoleConfig>
    {
        public void Configure(EntityTypeBuilder<UserRoleConfig> builder)
        {
            throw new NotImplementedException();
        }
    }
}
