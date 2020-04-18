using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.DataContext
{
    internal static class ModelBuilderExtensions
    {
        public static void ApplyTableNames(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableMetadata = entity.ClrType.GetCustomAttribute<EntityMetadataAttribute>();
                if (tableMetadata != null)
                {
                    modelBuilder.Entity(entity.Name).ToTable(tableMetadata.Name);
                }
            }
        }

        public static void IgnoreEvents(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                // TODO:
                if (typeof(BaseEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.Name).Ignore(nameof(BaseEntity.Events));
                }
            }
        }
    }
}
